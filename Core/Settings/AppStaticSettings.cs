namespace Core.Settings
{
    public class AppStaticSettings
    {
        public TelegramBotSettings TelegramBot { get; set; }
    }

    public class TelegramBotSettings
    {
        public string Token { get; set; }
        public string BaseUrl { get; set; }

        public Db Db { get; set; }

        public string PublicApiBaseUrl { get; set; }

        public string AndroidAppUrl { get; set; }
        public string IosAppUrl { get; set; }
        public string SupportMail { get; set; }
        public string FaqUrl { get; set; }
    }

    public class Db
    {
        public string TemplatesConnString { get; set; }
        public string DataConnString { get; set; }
        public string LogsConnString { get; set; }
        public string SharedConnString { get; set; }
    }
}
