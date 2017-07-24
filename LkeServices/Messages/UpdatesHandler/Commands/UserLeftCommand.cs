using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Telegram;

namespace LkeServices.Messages.UpdatesHandler.Commands
{
    public class UserLeftCommand : IBotCommand
    {
        private readonly IUsersOnChannelRepository _usersOnChannelRepository;

        public UserLeftCommand(IUsersOnChannelRepository usersOnChannelRepository)
        {
            _usersOnChannelRepository = usersOnChannelRepository;
        }

        public IEnumerable<string> SupportedCommands
        {
            get { yield return BotCommands.UserLeft; }
        }

        public async Task ExecuteCommand(string chatId, User userJoined, User userLeft)
        {
            if (userLeft == null)
                throw new Exception(nameof(userLeft));

            await _usersOnChannelRepository.TryRemoveAsync(userLeft.Id);
        }
    }
}
