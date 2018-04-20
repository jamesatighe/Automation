using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Automation.Models
{
    public class SCOMAlert
    {
        public int ID { get; set; }
        public string AlertHealth { get; set; }
        public string AlertName { get; set; }
        public string MonitorPath { get; set; }
        public string MonitoredObject { get; set; }
        public string MonitoredObjectHealth { get; set; }
        public string PrincipalName { get; set; }
        public DateTime Created { get; set; }
        public Nullable<DateTime> Resolved { get; set; }
        public string ResolvedBy { get; set; }
        public bool IsMonitorAlert { get; set; }
    }
}