using System.Threading.Tasks;

namespace Core.Telegram
{
    public interface IUsersOnChannelRepository
    {
        Task<bool> TryAddUserAsync(string id);
        Task TryRemoveAsync(string id);
    }
}
