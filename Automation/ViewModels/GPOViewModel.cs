using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Automation.ViewModels
{
    public class GPOViewModel
    {
        public string Domain { get; set; }
        public string GPOId { get; set; }
        public string Status { get; set; }
        public int Error { get; set; }
        public int Healthy { get; set; }
    }
}