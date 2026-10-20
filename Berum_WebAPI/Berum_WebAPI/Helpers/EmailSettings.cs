namespace Berum_WebAPI.Helpers
{
    public class EmailSettings
    {
        public string Host { get; set; } = "";
        public int Port { get; set; }
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public string From { get; set; } = "";
        public bool sslGmail { get; set; } = true;
        public bool defaultcredentialsGmail { get; set; } = false;
    
    }
}
