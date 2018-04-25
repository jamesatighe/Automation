using Automation.Models;
using Automation.ViewModels;
using System.Web.Mvc;
using Automation.DAL;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;


namespace Automation.Controllers
{
    public class SCOMController : Controller
    {

        private AutomationContext db = new AutomationContext();

        // GET: SCOM
        public async Task<ActionResult> Index()
        {
            return View(await db.SCOMAlert.ToListAsync());
        }

        // GET: SCOM/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SCOM/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SCOM/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SCOM/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SCOM/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SCOM/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SCOM/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [OutputCache(NoStore = true, Location = System.Web.UI.OutputCacheLocation.Client, Duration = 3)]
        public async Task<ActionResult> GetPartial(string partialName)
        {
            if (partialName == "_SCOM")
            {
                SCOM scom = db.SCOM.OrderByDescending(o => o.ID).FirstOrDefault();
                return PartialView(partialName, scom);
            }
            else if (partialName == "_SCOMAlertNew")
            {
                var viewModel = new List<SCOMNewViewModel>();
                List<SCOMAlert> scomalert = await db.SCOMAlert.ToListAsync();

                foreach (var alert in scomalert)
                {
                    viewModel.Add(new ViewModels.SCOMNewViewModel()
                    {
                        AlertHealth = alert.AlertHealth,
                        AlertName = alert.AlertName,
                        Created = alert.Created,
                        MonitoredObject = alert.MonitoredObject,
                        MonitorPath = alert.MonitorPath,
                        PrincipalName = alert.PrincipalName
                    });
                }
                return PartialView(partialName, viewModel);
            }
            else
            {

                var viewModel = new List<SCOMResolvedViewModel>();
                List<SCOMAlert> scomalert = await db.SCOMAlert.ToListAsync();

                foreach (var alert in scomalert)
                {
                    viewModel.Add(new ViewModels.SCOMResolvedViewModel()
                    {
                        AlertName = alert.AlertName,
                        MonitorPath = alert.MonitorPath,
                        MonitoredObject = alert.MonitoredObject,
                        Resolved = alert.Resolved,
                        ResolvedBy = alert.ResolvedBy,
                        MonitoredObjectHealth = alert.MonitoredObjectHealth,
                        IsMonitorAlert = alert.IsMonitorAlert

                    });
                      
                }
                return PartialView(partialName, viewModel);
            }
        }
    }
}
