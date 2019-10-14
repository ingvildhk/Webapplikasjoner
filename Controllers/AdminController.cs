using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using Oppg1.Metoder;
using System.Web.Script.Serialization;

namespace Oppg1.Controllers
{
    public class AdminController : Controller
    {
        
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

        //Oversikt avganger til stasjoner
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
                //sjekker at stasjonen ikke finnes fra før
                bool nyStasjonOK = stasjonDB.sjekkStasjonOK(endreStasjon);
                if (nyStasjonOK)
                {
                    bool endringOK = stasjonDB.endreStasjon(id, endreStasjon);
                    if (endringOK)
                    {
                        return RedirectToAction("OversiktStasjoner");
                    }
                }
            }
            return View();
        }

        public ActionResult EndreBane(int id)
        {
            var vyDB = new VyBLL();
            bane enbane = vyDB.hentEnBane(id);
            return View(enbane);
        }

        [HttpPost]
        public ActionResult EndreBane(int id, bane endreBane)
        {
            if (ModelState.IsValid)
            {
                var baneDB = new VyBLL();
                //sjekker at bane ikke finnes fra før
                bool nyBaneOK = baneDB.sjekkBaneOK(endreBane);
                if (nyBaneOK)
                {
                    bool endringOK = baneDB.endreBane(id, endreBane);
                    if (endringOK)
                    {
                        return RedirectToAction("OversiktBaner");
                    }
                }
            }
            return View();
        }

        public ActionResult EndreAvgang(int id)
        {
            var vyDB = new VyBLL();
            var enAvgang = vyDB.hentEnAvgang(id);
            return View(enAvgang);
        }

        [HttpPost]
        public ActionResult EndreAvgang(int id, stasjonPaaBane endreStasjonPaaBane)
        {
            if (ModelState.IsValid)
            {
                //sjekker at tidspunkt er på riktig format
                var metodeSjekk = new ValideringsMetoder();
                bool tidspunktOk = metodeSjekk.sjekkTidspunkt(endreStasjonPaaBane.Avgang);
                if (tidspunktOk)
                {
                    var vyDB = new VyBLL();
                    var bane = vyDB.hentEnBane(endreStasjonPaaBane.BaneID);
                    endreStasjonPaaBane.Bane = bane.Banenavn;
                    //sjekker at avgangen ikke finnes fra før (virker ikke enda da man ikke får med seg stasjonid og baneid fra httppost)
                    bool nyAvgangOK = vyDB.sjekkAvgangOK(endreStasjonPaaBane);
                    if (nyAvgangOK)
                    {
                        bool endringOK = vyDB.endreStasjonPaaBane(endreStasjonPaaBane, id);
                        if (endringOK)
                        {
                            //må endre denne til oversikt over avgang på stasjon
                            return RedirectToAction("OversiktStasjoner");
                        }
                    }
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

        public ActionResult SlettBane(int id)
        {
            var vyDB = new VyBLL();
            var enBane = vyDB.hentEnBane(id);
            return View(enBane);
        }

        [HttpPost]
        public ActionResult SlettBane (int id, bane enBane)
        {
            var vyDB = new VyBLL();
            bool slettOK = vyDB.slettBane(id);
            if (slettOK)
            {
                return RedirectToAction("OversiktBaner");
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
                //sjekker at stasjon ikke finnes fra før
                bool nyStasjonOK = vyDB.sjekkStasjonOK(stasjon);
                if (nyStasjonOK)
                {
                    bool leggTilOK = vyDB.leggTilStasjon(stasjon);
                    if (leggTilOK)
                    {
                        return RedirectToAction("OversiktStasjoner");
                    }
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
                //sjekker at banen ikke finnes fra før
                bool nyBaneOK = vyDB.sjekkBaneOK(bane);
                if (nyBaneOK)
                {
                    bool leggTilOK = vyDB.leggTilBane(bane);
                    if (leggTilOK)
                    {
                        return RedirectToAction("OversiktBaner");
                    }
                }
            }
            return View();
        }

        // Helt lik metode i homecontroller, må vi ha en her også?
        public string hentAlleStasjoner()
        {
            var BLL = new VyBLL();
            List<String> alleStasjoner = BLL.hentAlleStasjonsNavn();
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(alleStasjoner);
            return json;
        }

        public string hentAlleBanenavn()
        {
            var BLL = new VyBLL();
            List<bane> alleBaner = BLL.hentAlleBanenavn();
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(alleBaner);
            return json;
        }
    }
}