using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Automation.Models
{
    public class PowerShell
    {
        public int ID { get; set; }
        public string ScriptName { get; set; }
        [MaxLength(500)]
        public string ScriptContent { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastRunDate { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}