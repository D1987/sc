using Server.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Dtos
{
    public class HostModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public string Os { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }

        public List<VM> Vms { get; set; }
        public List<App> Apps { get; set; }
    }
}
