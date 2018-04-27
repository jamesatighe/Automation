using Automation.Models;
using Automation.ViewModels;
using System.Web.Mvc;
using Automation.DAL;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System;
using PagedList;
using PagedList.Mvc;

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
        public async Task<PartialViewResult> GetPartial(string partialName, string sortOrder, int? page, int? pageLength)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.PageLength = pageLength;

            if (partialName == "_SCOM")
            {
                //Get new alerts and add to ViewBag for display.
                DateTime date = new DateTime(1753,1,1);
                List<SCOMAlert> scomalert = await db.SCOMAlert.Where(s => s.Resolved == date).ToListAsync();
                ViewBag.SCOMNew = scomalert.Count();
                var scom = await db.SCOM.ToListAsync();
                


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

                if (!String.IsNullOrWhiteSpace(sortOrder))
                {
                    if (sortOrder == "name_desc")
                        viewModel = viewModel.OrderByDescending(s => s.AlertName).ToList();
                    else if (sortOrder == "date")
                        viewModel = viewModel.OrderBy(s => s.Created).ToList();
                    else if (sortOrder == "date_desc")
                        viewModel = viewModel.OrderByDescending(s => s.Created).ToList();
                    else if (sortOrder == "alerthealth")
                        viewModel = viewModel.OrderBy(s => s.AlertHealth).ToList();
                    else if (sortOrder == "alerthealth_desc")
                        viewModel = viewModel.OrderByDescending(s => s.AlertHealth).ToList();
                    else if (sortOrder == "monitorpath")
                        viewModel = viewModel.OrderBy(s => s.MonitorPath).ToList();
                    else if (sortOrder == "monitorpath_desc")
                        viewModel = viewModel.OrderByDescending(s => s.MonitorPath).ToList();
                    else if (sortOrder == "monitorobject")
                        viewModel = viewModel.OrderBy(s => s.MonitoredObject).ToList();
                    else if (sortOrder == "monitorobject_desc")
                        viewModel = viewModel.OrderByDescending(s => s.MonitoredObject).ToList();
                    else if (sortOrder == "principalname")
                        viewModel = viewModel.OrderBy(s => s.PrincipalName).ToList();
                    else if (sortOrder == "principalname_desc")
                        viewModel = viewModel.OrderByDescending(s => s.PrincipalName).ToList();
                }
                else
                { 
                    viewModel = viewModel.OrderBy(s => s.AlertName).ToList();
                }

                int pageSize = pageLength ?? 5;
                int pageNumber = page ?? 1;
                return PartialView(partialName, viewModel.ToPagedList(pageNumber, pageSize));
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

        public void ParentViewBag(string message)
        {
            ViewBag.AlertName = message;
        }
    }
}
