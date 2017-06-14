using System.Threading.Tasks;

namespace Core.Telegram
{
    public interface IOffsetRepository
    {
        Task<int> GetOffset();
        Task SetOffset(int offset);
    }
}
