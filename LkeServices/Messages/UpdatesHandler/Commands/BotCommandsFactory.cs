using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Telegram;

namespace LkeServices.Messages.UpdatesHandler.Commands
{
    public interface IBotCommand
    {
        IEnumerable<string> SupportedCommands { get; }

        Task ExecuteCommand(string chatId, User userJoined, User userLeft);
    }

    public class BotCommandsFactory
    {
        private readonly IEnumerable<IBotCommand> _commands;

        public BotCommandsFactory(IEnumerable<IBotCommand> commands)
        {
            _commands = commands;
        }

        public IBotCommand GetCommand(string botCommand = null)
        {
            return _commands.FirstOrDefault(command => command.SupportedCommands.Contains(botCommand));
        }
    }
}
