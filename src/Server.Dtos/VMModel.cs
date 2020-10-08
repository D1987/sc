using Server.Entities.Models;
namespace Server.Dtos
{
    public class VMModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public string Os { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public bool Critical { get; set; }
        public bool Enabled { get; set; }

        public int HostId { get; set; }
        public Host Host { get; set; }
    }
}
