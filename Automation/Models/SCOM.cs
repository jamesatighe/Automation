using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Automation.Models
{
    public class SCOM
    {
        public int ID { get; set; }
        public DateTime Time { get; set; }
        public bool Cluster { get; set; }
        public bool Server { get; set; }
        public bool RepGroup { get; set; }
        public string HealthState { get; set; }
        public bool InMaintenanceMode { get; set; }
        public string DisplayName { get; set; }
    }
}