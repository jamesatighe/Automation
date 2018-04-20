using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Automation.ViewModels
{
    public class SCOMNewViewModel
    {
        public string AlertHealth { get; set; }
        public string AlertName { get; set; }
        public string MonitorPath { get; set; }
        public string MonitoredObject { get; set; }
        public string PrincipalName { get; set; }
        public DateTime Created { get; set; }
    }
}