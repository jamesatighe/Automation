using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using Automation.DAL;

namespace Automation.Controllers
{
    public class ADController : Controller
    {
        private AutomationContext db = new AutomationContext();
        // GET: AD
        public async Task<ActionResult> Index()
        {
            return View(await db.AD.ToListAsync());
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
