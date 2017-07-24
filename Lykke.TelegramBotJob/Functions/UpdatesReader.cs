using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Messages;
using Core.Telegram;
using LkeServices.Messages.UpdatesHandler.Commands;
using Lykke.JobTriggers.Triggers.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Lykke.TelegramBotJob.Functions
{
    public class UpdatesReader
    {
        private readonly IHandledMessagesRepository _handledMessagesRepository;
        private readonly IUpdatesHandlerService _updatesHandlerService;
        private readonly IMessagesService _messagesService;
        private readonly TelegramBotClient _telegramBotClient;
        private readonly IOffsetRepository _offsetRepository;

        public UpdatesReader(IHandledMessagesRepository handledMessagesRepository,
            IUpdatesHandlerService updatesHandlerService,
            IMessagesService messagesService,
            TelegramBotClient telegramBotClient,
            IOffsetRepository offsetRepository)
        {
            _handledMessagesRepository = handledMessagesRepository;
            _updatesHandlerService = updatesHandlerService;
            _messagesService = messagesService;
            _telegramBotClient = telegramBotClient;
            _offsetRepository = offsetRepository;
        }

        [TimerTrigger("00:00:01")]
        public async Task GetUpdates()
        {
            var offset = await _offsetRepository.GetOffset();
            var updates = await _telegramBotClient.GetUpdatesAsync(offset);

            int maxOffset = 0;
            foreach (var update in updates)
            {
                bool offsetWasUpdated = false;
                if (update.Id > maxOffset)
                {
                    maxOffset = update.Id;
                    offsetWasUpdated = true;
                }

                var message = update.Message;

                if (message == null)
                    continue;

                var usrJoined = message.NewChatMember != null
                    ? new Core.Telegram.User
                    {
                        FirstName = message.NewChatMember.FirstName,
                        Id = message.NewChatMember.Id,
                        LastName = message.NewChatMember.LastName
                    }
                    : null;

                var usrLeft = message.LeftChatMember != null
                    ? new Core.Telegram.User
                    {
                        FirstName = message.LeftChatMember.FirstName,
                        Id = message.LeftChatMember.Id,
                        LastName = message.LeftChatMember.LastName
                    }
                    : null;

                var cmd = usrJoined != null
                    ? BotCommands.UserJoined
                    : usrLeft != null ? BotCommands.UserLeft : string.Empty;

                if (string.IsNullOrWhiteSpace(cmd))
                {
                    if (message.Entities.Any())
                    {
                        foreach (var entity in message.Entities)
                        {
                            if (entity.Type == MessageEntityType.BotCommand)
                            {
                                cmd = message.Text.Trim();
                                if (cmd.Contains('@'))
                                    cmd = cmd.Substring(0, cmd.IndexOf('@'));
                            }
                        }
                    }
                    else
                    {
                        cmd = BotCommands.TextCommands.FirstOrDefault(c => c == message.Text);
                    }
                }

                if (await _handledMessagesRepository.TryHandleMessage(message.MessageId))
                {
                    if (DateTime.UtcNow - message.Date < TimeSpan.FromMinutes(1))
                    {
                        if ((message.Chat.Type == ChatType.Group || message.Chat.Type == ChatType.Supergroup) && cmd != BotCommands.UserJoined && cmd != BotCommands.UserLeft)
                        {
                            var msg = await _messagesService.GetGroupMsg();
                            await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, msg);
                        }
                        else
                        {
                            await _updatesHandlerService.HandleUpdate(
                                message.Chat.Id, cmd,
                                usrJoined, usrLeft);
                        }
                    }
                }

                if (offsetWasUpdated)
                    await _offsetRepository.SetOffset(maxOffset);
            }
        }
    }
}
