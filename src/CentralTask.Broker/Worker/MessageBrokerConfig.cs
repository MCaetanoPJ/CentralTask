namespace CentralTask.Core.DTO.Worker
{
    public class MessageBrokerConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Virtualhost { get; set; }
    }
}