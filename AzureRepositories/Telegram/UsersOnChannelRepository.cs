using System.Threading.Tasks;
using AzureStorage;
using Core.Telegram;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureRepositories.Telegram
{
    public class UserOnChannelRecord : TableEntity
    {
        public static string GenerateParition()
        {
            return "user_ch";
        }

        public static string GenerateRowKey(string id)
        {
            return id;
        }

        public static UserOnChannelRecord Create(string id)
        {
            return new UserOnChannelRecord
            {
                PartitionKey = GenerateParition(),
                RowKey = GenerateRowKey(id)
            };
        }
    }

    public class UsersOnChannelRepository : IUsersOnChannelRepository
    {
        private readonly INoSQLTableStorage<UserOnChannelRecord> _tableStorage;

        public UsersOnChannelRepository(INoSQLTableStorage<UserOnChannelRecord> tableStorage)
        {
            _tableStorage = tableStorage;
        }

        public async Task<bool> TryAddUserAsync(string id)
        {
            var existing = await _tableStorage.GetDataAsync(UserOnChannelRecord.GenerateParition(),
                UserOnChannelRecord.GenerateRowKey(id));

            if (existing != null)
                return false;

            await _tableStorage.InsertAsync(UserOnChannelRecord.Create(id));
            return true;
        }

        public async Task TryRemoveAsync(string id)
        {
            var existing = await _tableStorage.GetDataAsync(UserOnChannelRecord.GenerateParition(),
                UserOnChannelRecord.GenerateRowKey(id));

            if (existing != null)
                await _tableStorage.DeleteAsync(UserOnChannelRecord.GenerateParition(),
                    UserOnChannelRecord.GenerateRowKey(id));
        }
    }
}
