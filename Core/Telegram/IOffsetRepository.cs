using System.Threading.Tasks;

namespace Core.Telegram
{
    public interface IOffsetRepository
    {
        /// <summary>
        /// Increments offset
        /// </summary>
        /// <returns>value befre increment</returns>
        Task<int> IncrementOffset();
    }
}
