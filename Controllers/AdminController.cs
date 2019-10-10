using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace Oppg1.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult OversiktStasjoner()
        {
            var vyDB = new VyBLL();
            List<Model.stasjon> alleStasjoner = vyDB.hentAlleStasjoner();
            return View(alleStasjoner);
        }


        public ActionResult OversiktBaner()
        {
            var vyDB = new VyBLL();
            List<Model.bane> alleBaner = vyDB.hentAlleBaner();
            return View(alleBaner);
        }

        public ActionResult EndreStasjon(int id)
        {
            var vyDB = new VyBLL();
            Model.stasjon enstasjon = vyDB.hentEnStasjon(id);
            return View(enstasjon);
        }

        [HttpPost]
        public ActionResult Endre(int id, Model.stasjon endreStasjon)
        {
            if (ModelState.IsValid)
            {
                var stasjonDB = new VyBLL();
                bool endringOK = stasjonDB.endreStasjon(id, endreStasjon);
                if (endringOK)
                {
                    return RedirectToAction("OversiktStasjoner");
                }
            }
            return View();
        }

        public ActionResult SlettStasjon()
        {
            return View();
        }

        public ActionResult EndreBane()
        {
            return View();
        }

        public ActionResult SlettBane()
        {
            return View();
        }
    }
}