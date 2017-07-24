using System.Threading.Tasks;
using Core.Settings;
using Core.Telegram;
using LkeServices.Messages.UpdatesHandler.Commands;

namespace LkeServices.Messages
{
    public class UpdateHandlerService : IUpdatesHandlerService
    {
        private readonly BotCommandsFactory _botCommandsFactory;

        public UpdateHandlerService(BotCommandsFactory botCommandsFactory)
        {
            _botCommandsFactory = botCommandsFactory;
        }

        public async Task HandleUpdate(string chatId, string botCommand, User userJoined, User userLeft)
        {
            var command = _botCommandsFactory.GetCommand(botCommand);
            if (command != null)
                await command.ExecuteCommand(chatId, userJoined, userLeft);
        }
    }
}
