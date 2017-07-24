using System.Threading.Tasks;

namespace Core.Telegram
{
    public interface IUpdatesHandlerService
    {
        Task HandleUpdate(string chatId, string botCommand, User userJoined, User userLeft);
    }

    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
