using System;

namespace Automation.Models
{
    public class AD
    {
        public int ADId { get; set; }
        public string Domain { get; set; }
        public string DomainController { get; set; }
        public string Connection { get; set; }
        public string RepTestState { get; set; }
        public string RepTestResult { get; set; }
        public string NetLogonTestState { get; set; }
        public string NetLogonTestResult { get; set; }
        public string NetLogonSvc { get; set; }
        public string NTDSSvc { get; set; }
        public string KDCSvc { get; set; }
        public string DNSSvc { get; set; }
        public string W32TimeSvc { get; set; }
        public string DFSRSvc { get; set; }
        public DateTime Created { get; set; }
    }
}