using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Automation.DAL;
using Automation.Models;
using System.Collections.ObjectModel;
using System.Management.Automation.Runspaces;
using System.Management.Automation;
using System.Text;

namespace Automation.Controllers
{
    public class PowerShellController : Controller
    {
        private AutomationContext db = new AutomationContext();

        // GET: PowerShell
        public async Task<ActionResult> Index()
        {
            return View(await db.PowerShell.ToListAsync());
        }


        // GET: ADCreation
        public ActionResult ADCreation(string Results, string FirstName, string LastName)
        {
            ViewBag.Results = Results;
            ViewBag.FirstName = FirstName;
            ViewBag.LastName = LastName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ADCreationPost(string LastName, string FirstName, bool? Admin, bool? Domain)
        {
            Runspace runspace = RunspaceFactory.CreateRunspace();

            //Open runspace
            runspace.Open();

            //Create Pipeline
            Pipeline pipeline = runspace.CreatePipeline();

            StringBuilder script = new StringBuilder();
            script.AppendLine("Import-Module ActiveDirectory");
            if (Admin == true && Domain == true)
            {
                script.AppendLine("Get-ADUSer -filter 'surname -like \"" + LastName + "\" -and givenName -like \"" + FirstName + "\" -and (Name -like \"*adm*\" -or Name -like \"*dom*\")'");
            }
            else if (Admin == true && (Domain == false || Domain == null))
            {
                script.AppendLine("Get-ADUSer -filter 'surname -like \"" + LastName + "\" -and givenName -like \"" + FirstName + "\" -and Name -like \"*adm*\"'");
            }
            else if ((Admin == false || Admin == null) && Domain == true)
            {
                script.AppendLine("Get-ADUSer -filter 'surname -like \"" + LastName + "\" -and givenName -like \"" + FirstName + "\" -and Name -like \"*dom*\"'");
            }
            else
            {
                script.AppendLine("Get-ADUSer -filter 'surname -like \"" + LastName + "\" -and givenName -like \"" + FirstName + "\"'");
            }

            var scripttext = script.ToString();
            pipeline.Commands.AddScript(scripttext);

            //Execute Script
            Collection<PSObject> results = pipeline.Invoke();

            if (results.Count > 0)
            {
                var builder = new StringBuilder();

                foreach (var PSObject in results)
                {
                    builder.Append(PSObject.BaseObject.ToString() + "\r\n");
                }

                ViewBag.Results = Server.HtmlEncode(builder.ToString());

            }
            ViewBag.FirstName = FirstName;
            ViewBag.LastName = LastName;
            //Close runspace
            runspace.Close();

            //After completed redirect to Index page
            return RedirectToAction("ADCreation", new { Results = ViewBag.Results, FirstName = ViewBag.FirstName, LastName = ViewBag.LastName });
        }

        // GET: PowerShell/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.PowerShell powerShell = await db.PowerShell.FindAsync(id);
            if (powerShell == null)
            {
                return HttpNotFound();
            }
            return View(powerShell);
        }

        // GET: PowerShell/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PowerShell/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ScriptName,ScriptContent,CreatedDate,LastRunDate")] Models.PowerShell powerShell)
        {
            if (ModelState.IsValid)
            {
                db.PowerShell.Add(powerShell);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(powerShell);
        }

        // GET: PowerShell/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.PowerShell powerShell = await db.PowerShell.FindAsync(id);
            if (powerShell == null)
            {
                return HttpNotFound();
            }
            return View(powerShell);
        }

        // POST: PowerShell/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ScriptName,ScriptContent,CreatedDate,LastRunDate")] Models.PowerShell powerShell)
        {
            if (ModelState.IsValid)
            {
                db.Entry(powerShell).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(powerShell);
        }

        // GET: PowerShell/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.PowerShell powerShell = await db.PowerShell.FindAsync(id);
            if (powerShell == null)
            {
                return HttpNotFound();
            }
            return View(powerShell);
        }

        // POST: PowerShell/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Models.PowerShell powerShell = await db.PowerShell.FindAsync(id);
            db.PowerShell.Remove(powerShell);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //Create Partial Views
        [OutputCache(NoStore = true, Location = System.Web.UI.OutputCacheLocation.Client, Duration = 3)]
        public async Task<ActionResult> GetPSPartial()
        {
            List<Models.PowerShell> powerShell = await db.PowerShell.ToListAsync();
            return PartialView("_PowerShell", powerShell);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
