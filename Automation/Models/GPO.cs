using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Automation.Models
{
    public class GPO
    {
        public int GPOId { get; set; }
        public string DomainName { get; set; }
        public string Name { get; set; }
        public string Guid { get; set; }
        public string Status { get; set; }
        public string UserADVer { get; set; }
        public string UserSysvolVer { get; set; }
        public string CompADVer { get; set; }
        public string CompSysvolVer { get; set; }
        public DateTime GPOCreated { get; set; }
        public DateTime GPOModified { get; set; }
        public DateTime Created { get; set; }
    }
}