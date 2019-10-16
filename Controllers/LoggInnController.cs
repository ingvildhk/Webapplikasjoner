using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BLL;


namespace Oppg1.Controllers
{
    public class LoggInnController : Controller
    {
        private IVyBLL _vyBLL;

        public LoggInnController()
        {
            _vyBLL = new VyBLL();
        }
        // GET: LoggInn
        public ActionResult LoggInn()
        {
            Session["Innlogget"] = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoggInn(bruker innlogget)
        {
            bool finnes = _vyBLL.finnBrukerDB(innlogget);
            // Sjekker om innlogging ok
            if (finnes){
                Session["Innlogget"] = true;
                return RedirectToAction("OversiktStasjoner", "Admin");
            }
            else
            {
                Session["Innlogget"] = false;
                return View();
                    
            }
        }

        public ActionResult LoggUt()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Index");
        }
    }
}