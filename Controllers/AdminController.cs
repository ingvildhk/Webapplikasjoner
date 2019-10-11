using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;

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
            List<bane> alleBaner = vyDB.hentAlleBaner();
            return View(alleBaner);
        }

        public ActionResult EndreStasjon(int id)
        {
            var vyDB = new VyBLL();
            stasjon enstasjon = vyDB.hentEnStasjon(id);
            return View(enstasjon);
        }

        [HttpPost]
        public ActionResult EndreStasjon(int id, stasjon endreStasjon)
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

        public ActionResult SlettStasjon(int id)
        {
            var vyDB = new VyBLL();
            stasjon enStasjon = vyDB.hentEnStasjon(id);
            return View(enStasjon);
        }

        [HttpPost]
        public ActionResult SlettStasjon(int id, stasjon slettstasjon)
        {
            var vyDB = new VyBLL();
            bool slettOK = vyDB.slettStasjon(id);
            if (slettOK)
            {
                return RedirectToAction("OversiktStasjoner");
            }
            return View();
        }

        public ActionResult LeggTilStasjon()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LeggTilStasjon(stasjon stasjon)
        {
            if (ModelState.IsValid)
            {
                var vyDB = new VyBLL();
                bool leggTilOK = vyDB.leggTilStasjon(stasjon);
                if (leggTilOK)
                {
                    return RedirectToAction("OversiktStasjoner");
                }
            }
            return View();
        }

        public ActionResult LeggTilBane()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LeggTilBane(bane bane)
        {
            if (ModelState.IsValid)
            {
                var vyDB = new VyBLL();
                bool leggTilOK = vyDB.leggTilBane(bane);
                if (leggTilOK)
                {
                    return RedirectToAction("OversiktBaner");
                }
            }
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

        public ActionResult AvgangerPaStasjon(int id)
        {
            var vyDB = new VyBLL();
            List<stasjonPaaBane> listen = vyDB.hentStasjonPaaBane(id);
            return View(listen);
        }

        public ActionResult LeggTilAvgang()
        {
            return View();
        }
    }
}