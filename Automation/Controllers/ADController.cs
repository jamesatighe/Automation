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
            ViewBag.Domain = domains.First();
            int UserError = 0;
            int CompError = 0;

            var viewModel = new List<GPOViewModel>();

            foreach (var item in gpo)
            {
                if (item.CompADVer != item.CompSysvolVer)
                {
                    CompError++;
                }
                if (item.UserADVer != item.UserSysvolVer)
                {
                    UserError++;
                }
            }
            foreach (var domain in domains)
            {
                var gpoError = 0;
                var gpoHealthy = 0;
                string GPOId = "";
                var errors = await gpo.Where(g => g.DomainName == domain.DomainName).ToListAsync();
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
 

                viewModel.Add(new ViewModels.GPOViewModel()
                {
                    Domain = domain.DomainName,
                    GPOId = GPOId,
                    Error = gpoError,
                    Healthy = gpoHealthy
                });
            }

            return PartialView("_GPChart", viewModel);
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
