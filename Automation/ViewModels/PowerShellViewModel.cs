using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Automation.ViewModels
{
    public class PowerShellViewModel
    {
        public string ScriptName { get; set; }
        [MaxLength(500)]
        public string ScriptContent { get; set; }
        public DateTime LastRunDate { get; set; }
    }
}