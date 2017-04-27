using System;
using System.Threading.Tasks;
using Core.Messages;
using Core.Settings;
using Core.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace LkeServices.Messages.UpdatesHandler.Commands
{
    public class UserJoinedCommand : IBotCommand
    {
        private readonly IMessagesService _messagesService;
        private readonly TelegramBotClient _telegramBotClient;
        private readonly IUsersOnChannelRepository _usersOnChannelRepository;

        public UserJoinedCommand(IMessagesService messagesService,
            TelegramBotClient telegramBotClient, IUsersOnChannelRepository usersOnChannelRepository)
        {
            _messagesService = messagesService;
            _telegramBotClient = telegramBotClient;
            _usersOnChannelRepository = usersOnChannelRepository;
        }

        public async Task ExecuteCommand(bool isGroup, string chatId, User userJoined, User userLeft)
        {
            if (userJoined == null)
                throw new Exception(nameof(userJoined));

            if (await _usersOnChannelRepository.TryAddUserAsync(userJoined.Id))
            {
                if (userJoined.Id == "354287494") //ToDo: remove hardcode for bot id
                    return;

                var msg = await _messagesService.GetWelcomeMsg(userJoined.FirstName, userJoined.LastName);
                await _telegramBotClient.SendTextMessageAsync(chatId, msg, ParseMode.Markdown);
            }
        }
    }
}
