using System;
using System.Threading.Tasks;
using AzureStorage;
using Common;
using Core.Messages;
using Microsoft.Extensions.Caching.Memory;

namespace AzureRepositories.Messages
{
    public class MessagesTemplatesRepository : IMessagesTemplatesRepository
    {
        private readonly IBlobStorage _blobStorage;
        private readonly IMemoryCache _memoryCache;

        private const string TemplatesCacheKey = "_TemplatesCacheKey_";
        private const string ContainerName = "templates";
        private const string TemplatesFile = "templates.txt";
        private readonly TimeSpan _cacheExpTime = TimeSpan.FromMinutes(5);

        public MessagesTemplatesRepository(IBlobStorage blobStorage, IMemoryCache memoryCache)
        {
            _blobStorage = blobStorage;
            _memoryCache = memoryCache;
        }

        public class Templates
        {
            public string WelcomeMsgTemplate { get; set; }
            public string StartGroupMsgTemplate { get; set; }
            public string StartPrivateMsgTemplate { get; set; }
            public string RatesMsgTemplate { get; set; }
            public string LkkPriceMsgTemplate { get; set; }
            public string GetAppMsgTemplate { get; set; }
            public string AndroidAppMsgTemplate { get; set; }
            public string IosAppMsgTemplate { get; set; }
            public string SupportMailMsgTemplate { get; set; }
            public string FaqMsgTemplate { get; set; }
            public string PairsMsgTemplate { get; set; }
        }

        private async Task<Templates> GetAllTemplates()
        {
            Templates record;

            if (!_memoryCache.TryGetValue(TemplatesCacheKey, out record))
            {
                if (!await _blobStorage.HasBlobAsync(ContainerName, TemplatesFile))
                {
                    return null;
                }

                record = (await _blobStorage.GetAsTextAsync(ContainerName, TemplatesFile)).DeserializeJson<Templates>();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(_cacheExpTime);

                _memoryCache.Set(TemplatesCacheKey, record, cacheEntryOptions);
            }

            return record;
        }

        public async Task<string> GetWelcomeMsgTemplate()
        {
            return (await GetAllTemplates()).WelcomeMsgTemplate;
        }

        public async Task<string> GetStartPrivateMsgTemplate()
        {
            return (await GetAllTemplates()).StartPrivateMsgTemplate;
        }

        public async Task<string> GetStartGroupMsgTemplate()
        {
            return (await GetAllTemplates()).StartGroupMsgTemplate;
        }

        public async Task<string> GetRatesMsgTemplate()
        {
            return (await GetAllTemplates()).RatesMsgTemplate;
        }

        public async Task<string> GetLkkPriceMsgTemplate()
        {
            return (await GetAllTemplates()).LkkPriceMsgTemplate;
        }

        public async Task<string> GetAppMsgTemplate()
        {
            return (await GetAllTemplates()).GetAppMsgTemplate;
        }

        public async Task<string> GetAndroidAppMsgTemplate()
        {
            return (await GetAllTemplates()).AndroidAppMsgTemplate;
        }

        public async Task<string> GetIosAppMsgTemplate()
        {
            return (await GetAllTemplates()).IosAppMsgTemplate;
        }

        public async Task<string> GetSupportMailMsgTemplate()
        {
            return (await GetAllTemplates()).SupportMailMsgTemplate;
        }

        public async Task<string> GetFaqMsgTemplate()
        {
            return (await GetAllTemplates()).FaqMsgTemplate;
        }

        public async Task<string> GetPairsMsgTemplate()
        {
            return (await GetAllTemplates()).PairsMsgTemplate;
        }
    }
}
