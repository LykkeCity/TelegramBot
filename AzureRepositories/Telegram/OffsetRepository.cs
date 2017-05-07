using System.Threading.Tasks;
using AzureStorage;
using Core.Telegram;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureRepositories.Telegram
{
    public class OffsetRecord : TableEntity
    {
        public static string GeneratePartition()
        {
            return "O";
        }

        public static string GenerateRowKey()
        {
            return "O";
        }

        public static OffsetRecord Create(int offset)
        {
            return new OffsetRecord
            {
                PartitionKey = GeneratePartition(),
                RowKey = GenerateRowKey(),
                Offset = offset
            };
        }

        public int Offset { get; set; }
    }

    public class OffsetRepository : IOffsetRepository
    {
        private readonly INoSQLTableStorage<OffsetRecord> _tableStorage;

        public OffsetRepository(INoSQLTableStorage<OffsetRecord> tableStorage)
        {
            _tableStorage = tableStorage;
        }

        public async Task<int> IncrementOffset()
        {
            var currentRecord = await _tableStorage.GetDataAsync(OffsetRecord.GeneratePartition(),
                OffsetRecord.GenerateRowKey()) ?? OffsetRecord.Create(0);

            currentRecord.Offset++;

            await _tableStorage.InsertOrReplaceAsync(currentRecord);

            return currentRecord.Offset - 1;
        }
    }
}
