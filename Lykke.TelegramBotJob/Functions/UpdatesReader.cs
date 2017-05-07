using System.Linq;
using System.Threading.Tasks;
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
        private readonly TelegramBotClient _telegramBotClient;
        private readonly IOffsetRepository _offsetRepository;

        public UpdatesReader(IHandledMessagesRepository handledMessagesRepository,
            IUpdatesHandlerService updatesHandlerService,
            TelegramBotClient telegramBotClient,
            IOffsetRepository offsetRepository)
        {
            _handledMessagesRepository = handledMessagesRepository;
            _updatesHandlerService = updatesHandlerService;
            _telegramBotClient = telegramBotClient;
            _offsetRepository = offsetRepository;
        }

        [TimerTrigger("00:00:01")]
        public async Task GetUpdates()
        {
            var updates = await _telegramBotClient.GetUpdatesAsync(await _offsetRepository.IncrementOffset());
            foreach (var update in updates)
            {
                var message = update.Message;

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
                    ? BotCommandsFactory.UserJoined
                    : usrLeft != null ? BotCommandsFactory.UserLeft : string.Empty;

                foreach (var entity in message.Entities)
                {
                    if (entity.Type == MessageEntityType.BotCommand)
                    {
                        cmd = message.Text.Trim();
                        if (cmd.Contains('@'))
                            cmd = cmd.Substring(0, cmd.IndexOf('@'));
                    }
                }

                if (await _handledMessagesRepository.TryHandleMessage(message.MessageId))
                {
                    await _updatesHandlerService.HandleUpdate(message.Chat.Type == ChatType.Group, message.Chat.Id, cmd,
                        usrJoined, usrLeft);
                }
            }
        }
    }
}
