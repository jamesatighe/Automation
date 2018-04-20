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
        public int NewError { get; set; }
        public int NewWarning { get; set; }
        public int TotalNew { get; set; }
        public int TotalClusters { get; set; }
        public int ClustersError { get; set; }
        public int ClustersWarning { get; set; }
        public int TotalServers { get; set; }
        public int ServersError { get; set; }
        public int ServersWarning { get; set; }
    }
}