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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IVyBLL _vyBLL;

        public LoggInnController()
        {
            _vyBLL = new VyBLL();
        }

        public LoggInnController(IVyBLL stub)
        {
            _vyBLL = stub;
        }

        // GET: LoggInn
        public ActionResult LoggInn()
        {
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
                log.Info("Innlogging registrert!");
                return RedirectToAction("OversiktStasjoner", "Admin");
            }
            else
            {
                Session["Innlogget"] = false;
                log.Warn("Innlogging feilet!");
                return View(); 
            }
        }

        public ActionResult LoggUt()
        {
            Session["Innlogget"] = false;
            return RedirectToAction("../Home/Index", "Home");
        }
    }
}