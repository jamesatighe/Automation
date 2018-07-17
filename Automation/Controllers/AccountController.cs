using Automation.DAL;
using Automation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Automation.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult LogOn()
        {
            return View();
        }

        private AutomationContext db = new AutomationContext();

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    var user = model.UserName;
                    var cache = db.Cache.Where(c => c.UserName == user);
                    if (cache.Count() > 0)
                    {
                        var tempModel = new Cache
                        {
                            ID = cache.FirstOrDefault().ID,
                            UserName = cache.FirstOrDefault().UserName,
                            HTMLCache = cache.FirstOrDefault().HTMLCache,
                            LastLogOn = DateTime.UtcNow,
                            LastLogOff = cache.FirstOrDefault().LastLogOff,
                        };

                        db.Entry(cache.FirstOrDefault()).CurrentValues.SetValues(tempModel);
                        db.SaveChanges();
                    }
                    else
                    {
                        var tempModel = new Cache
                        {
                            UserName = user,
                            HTMLCache = "",
                            LastLogOn = DateTime.UtcNow,
                            LastLogOff = null,
                        };
                        db.Cache.Add(tempModel);
                        db.SaveChanges();
                    }

                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("\\") )
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect");
                }
            }

            // if we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}