using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using Automation.DAL;
using Automation.Models;
using Automation.ViewModels;
using X.PagedList;
using X.PagedList.Mvc.Bootstrap4;
using System.Collections.Generic;
using System.Linq;

namespace Automation.Controllers
{
    public class ADController : Controller
    {
        private AutomationContext db = new AutomationContext();
        // GET: AD
        public async Task<PartialViewResult> Index(string sortOrder, int? page, int? pageLength)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.PageLength = pageLength;

            List<AD> ad = await db.AD.ToListAsync();
            int pageSize = pageLength ?? 5;
            int pageNumber = page ?? 1;
            return PartialView("_AD", (IPagedList)ad.ToPagedList(pageNumber, pageSize));
        }

        public async Task<PartialViewResult> GPOIndex(string sortOrder, int? page, int? pageLength)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.PageLength = pageLength;

            List<GPO> gpo = await db.GPO.ToListAsync();
            int pageSize = pageLength ?? 5;
            int pageNumber = page ?? 1;

            return PartialView("_gpo", (IPagedList)gpo.ToPagedList(pageNumber, pageSize));
        }

        public async Task<PartialViewResult> GPOChart()
        {
            List<GPO> gpo = await db.GPO.ToListAsync();
            var domains = gpo.GroupBy(x => x.DomainName).Select(g => g.FirstOrDefault());
            ViewBag.Domain = domains.First().DomainName;

            var viewModel = new GPOViewModel();

            var gpoError = 0;
            var gpoHealthy = 0;
            string GPOId = "";
            var errors = await gpo.Where(g => g.DomainName == ViewBag.Domain).ToListAsync();
            foreach (var error in errors)
            {
                if ((error.UserADVer != error.UserSysvolVer || error.CompADVer != error.CompSysvolVer))
                {
                    gpoError++;
                }
                else
                {
                    gpoHealthy++;
                }
                GPOId = error.GPOId.ToString();
                    
            }

            viewModel.Domain = ViewBag.Domain;
            viewModel.GPOId = GPOId;
            viewModel.Error = gpoError;
            viewModel.Healthy = gpoHealthy;

            return PartialView("_GPChart", viewModel);
        }

        public async Task<PartialViewResult> ADServiceHealth(string DomainChoice)
        {
            ViewBag.DomainList = new SelectList(db.AD.GroupBy(a => a.Domain).ToList());

            List<AD> ad = await db.AD.ToListAsync();
            if (ViewBag.DomainChoice == null)
            {
                var domains = ad.GroupBy(x => x.Domain).Select(a => a.FirstOrDefault());
                ViewBag.DomainChoice = domains.FirstOrDefault().Domain;
            }
            else
            {
                ViewBag.DomainChoice = DomainChoice;
            }
            var domain = await ad.Where(a => a.Domain == ViewBag.DomainChoice).ToListAsync();
            var viewModel = new List<ADServiceHealthViewModel>();
    

            foreach (var item in domain)
            {
                var serviceErrors = 0;

                if (item.KDCSvc != "Running")
                    serviceErrors++;
                if (item.NetLogonSvc != "Running")
                    serviceErrors++;
                if (item.NTDSSvc != "Running")
                    serviceErrors++;
                if (item.W32TimeSvc != "Running")
                    serviceErrors++;
                if (item.DNSSvc != "Running")
                    serviceErrors++;
                if (item.DFSRSvc != "Running")
                    serviceErrors++;

                viewModel.Add(new ADServiceHealthViewModel()
                {
                    Domain = item.Domain,
                    DomainController = item.DomainController,
                    ServiceErrors = serviceErrors
                });
            }

            return PartialView("_ADServiceHealth", viewModel);
        }

        // GET: AD/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AD/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AD/Create
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

        // GET: AD/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AD/Edit/5
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

        // GET: AD/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AD/Delete/5
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
    }
}
