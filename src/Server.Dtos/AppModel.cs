using Server.Entities.Models;

namespace Server.Dtos
{
    public class AppModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public string Domain { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Project { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool Critical { get; set; }
        public bool Enabled { get; set; }

        public int HostId { get; set; }
        public Host Host { get; set; }

        public int VmId { get; set; }
        public VM Vm { get; set; }
    }
}
