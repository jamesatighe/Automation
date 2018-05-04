namespace Automation.ViewModels
{
    public class ADServiceHealthViewModel
    {
        public string Domain { get; set; }
        public string DomainController { get; set; }
        public int ServiceErrors { get; set; }
    }
}