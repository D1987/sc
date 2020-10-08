using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Server.Entities.Models
{
    public class Host
    {
        public Host()
        {
            Vms = new Collection<VM>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public string Os { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }

        public ICollection<VM> Vms { get; set; }
    }
}
