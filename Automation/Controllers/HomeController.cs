using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Web;
using System.Web.Mvc;

namespace Automation.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult PowerShell(string scripttext)
        {
            Runspace runspace = RunspaceFactory.CreateRunspace();

            //Open runspace
            runspace.Open();

            //Create Pipeline
            Pipeline pipeline = runspace.CreatePipeline();

            pipeline.Commands.AddScript(scripttext);

            //Execute Script
            Collection<PSObject> results = pipeline.Invoke();

            //Close runspace
            runspace.Close();

            //After completed redirect to Index page
            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}