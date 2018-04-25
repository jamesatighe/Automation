using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Automation.ViewModels
{
    public class SCOMResolvedViewModel
    {
        public string AlertName { get; set; }
        public string MonitorPath { get; set; }
        public string MonitoredObject { get; set; }
        public string MonitoredObjectHealth { get; set; }
        public Nullable<DateTime> Resolved { get; set; }
        public string ResolvedBy { get; set; }
        public bool IsMonitorAlert { get; set; }
    }
}