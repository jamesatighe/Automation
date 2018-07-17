using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automation.Models
{
    public class Cache
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        [AllowHtml]
        public string HTMLCache { get; set; }
        public DateTime LastLogOn { get; set; }
        public DateTime? LastLogOff { get; set; }
    }
}