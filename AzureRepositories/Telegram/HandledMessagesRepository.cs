using System.Threading.Tasks;
using AzureStorage;
using Core.Telegram;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureRepositories.Telegram
{
    public class HandledMessageRecord : TableEntity
    {
        public static string GenerateParition()
        {
            return "handled";
        }

        public static string GenerateRowKey(long id)
        {
            return id.ToString();
        }

        public static HandledMessageRecord Create(long id)
        {
            return new HandledMessageRecord
            {
                PartitionKey = GenerateParition(),
                RowKey = GenerateRowKey(id)
            };
        }
    }

    public class HandledMessagesRepository : IHandledMessagesRepository
    {
        private readonly INoSQLTableStorage<HandledMessageRecord> _tableStorage;

        public HandledMessagesRepository(INoSQLTableStorage<HandledMessageRecord> tableStorage)
        {
            _tableStorage = tableStorage;
        }

        public async Task<bool> TryHandleMessage(int id)
        {
            var existing = await _tableStorage.GetDataAsync(HandledMessageRecord.GenerateParition(),
                HandledMessageRecord.GenerateRowKey(id));

            if (existing != null)
                return false;

            await _tableStorage.InsertAsync(HandledMessageRecord.Create(id));

            return true;
        }
    }
}
