using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Automation.Models
{
    public class Permission
    {
        public int PermissionID { get; set; }
        public int PowerShellID { get; set; }
        public string GroupName { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
        public bool Modify { get; set; }
    }
}