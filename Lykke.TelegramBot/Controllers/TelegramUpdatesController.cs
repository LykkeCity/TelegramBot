using System.Linq;
using System.Threading.Tasks;
using Core.Messages;
using Core.Settings;
using Core.Telegram;
using LkeServices.Messages.UpdatesHandler.Commands;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Lykke.TelegramBot.Controllers
{
    [Route("api/[controller]")]
    public class TelegramUpdatesController : Controller
    {
        private readonly IHandledMessagesRepository _handledMessagesRepository;
        private readonly IUpdatesHandlerService _updatesHandlerService;

        public TelegramUpdatesController(IHandledMessagesRepository handledMessagesRepository,
            IUpdatesHandlerService updatesHandlerService)
        {
            _handledMessagesRepository = handledMessagesRepository;
            _updatesHandlerService = updatesHandlerService;
        }

        [HttpPost]
        public async Task Post([FromBody]Update update)
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
