using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BLL;
using Model;
using Oppg1.Metoder;

namespace Oppg1.Controllers
{
    public class AdminController : Controller
    {

        private IVyBLL _vyBLL;

        public AdminController()
        {
            _vyBLL = new VyBLL();
        }

        public AdminController(IVyBLL stub)
        {
            _vyBLL = stub;
        }
        
        [SessionSjekker]
        public ActionResult OversiktStasjoner()
        {
            List<stasjon> alleStasjoner = _vyBLL.hentAlleStasjoner();
            return View(alleStasjoner);
        }

        [SessionSjekker]
        public ActionResult OversiktBaner()
        {
            List<bane> alleBaner = _vyBLL.hentAlleBaner();
            return View(alleBaner);
        }

        [SessionSjekker]
        //Oversikt avganger til stasjoner
        public ActionResult AvgangerPaStasjon(int id)
        {
            List<stasjonPaaBane> listen = _vyBLL.hentStasjonPaaBane(id);
            return View(listen);
        }

        [SessionSjekker]
        public ActionResult EndreStasjon(int id)
        {
            stasjon enstasjon = _vyBLL.hentEnStasjon(id);
            return View(enstasjon);
        }

        [HttpPost]
        public ActionResult EndreStasjon(int id, stasjon endreStasjon)
        {
            if (ModelState.IsValid)
            {
                //sjekker at stasjonen ikke finnes fra før
                bool nyStasjonOK = _vyBLL.sjekkStasjonOK(endreStasjon);
                if (nyStasjonOK)
                {
                    bool endringOK = _vyBLL.endreStasjon(id, endreStasjon);
                    if (endringOK)
                    {
                        return RedirectToAction("OversiktStasjoner");
                    }
                }
            }
            return View();
        }

        [SessionSjekker]
        public ActionResult EndreBane(int id)
        {
            bane enbane = _vyBLL.hentEnBane(id);
            return View(enbane);
        }

        [HttpPost]
        public ActionResult EndreBane(int id, bane endreBane)
        {
            if (ModelState.IsValid)
            {
                //sjekker at bane ikke finnes fra før
                bool nyBaneOK = _vyBLL.sjekkBaneOK(endreBane);
                if (nyBaneOK)
                {
                    bool endringOK = _vyBLL.endreBane(id, endreBane);
                    if (endringOK)
                    {
                        return RedirectToAction("OversiktBaner");
                    }
                }
            }
            return View();
        }

        [SessionSjekker]
        public ActionResult EndreAvgang(int id)
        {
            var enAvgang = _vyBLL.hentEnAvgang(id);
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
                    var bane = _vyBLL.hentEnBane(endreStasjonPaaBane.BaneID);
                    endreStasjonPaaBane.Bane = bane.Banenavn;
                    //sjekker at avgangen ikke finnes fra før 
                    bool nyAvgangOK = _vyBLL.sjekkAvgangOK(endreStasjonPaaBane);
                    if (nyAvgangOK)
                    {
                        bool endringOK = _vyBLL.endreStasjonPaaBane(endreStasjonPaaBane, id);
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

        [SessionSjekker]
        public ActionResult SlettStasjon(int id)
        {
            stasjon enStasjon = _vyBLL.hentEnStasjon(id);
            return View(enStasjon);
        }

        [HttpPost]
        public ActionResult SlettStasjon(int id, stasjon slettstasjon)
        {
            bool slettOK = _vyBLL.slettStasjon(id);
            if (slettOK)
            {
                return RedirectToAction("OversiktStasjoner");
            }
            return View();
        }

        [SessionSjekker]
        public ActionResult SlettBane(int id)
        {
            var enBane = _vyBLL.hentEnBane(id);
            return View(enBane);
        }

        [HttpPost]
        public ActionResult SlettBane (int id, bane enBane)
        {
            bool slettOK = _vyBLL.slettBane(id);
            if (slettOK)
            {
                return RedirectToAction("OversiktBaner");
            }
            return View();
        }

        [SessionSjekker]
        public ActionResult SlettAvgang(int id)
        {
            var enAvgang = _vyBLL.hentEnAvgang(id);
            return View(enAvgang);
        }

        [HttpPost]
        public ActionResult SlettAvgang (int id, stasjonPaaBane avgang)
        {
            var baneidTilAvgang = _vyBLL.hentEnAvgang(id);

            bool slettOK = _vyBLL.slettStasjonPaaBane(id, baneidTilAvgang.BaneID);
            if (slettOK)
            {
                return RedirectToAction("OversiktStasjoner");
            }
            return View();

        }

        [SessionSjekker]
        public ActionResult LeggTilStasjon()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LeggTilStasjon(stasjon stasjon)
        {
            if (ModelState.IsValid)
            {
                //sjekker at stasjon ikke finnes fra før
                bool nyStasjonOK = _vyBLL.sjekkStasjonOK(stasjon);
                if (nyStasjonOK)
                {
                    bool leggTilOK = _vyBLL.leggTilStasjon(stasjon);
                    if (leggTilOK)
                    {
                        return RedirectToAction("OversiktStasjoner");
                    }
                }               
            }
            return View();
        }

        [SessionSjekker]
        public ActionResult LeggTilBane()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LeggTilBane(bane bane)
        {
            if (ModelState.IsValid)
            {
                //sjekker at banen ikke finnes fra før
                bool nyBaneOK = _vyBLL.sjekkBaneOK(bane);
                if (nyBaneOK)
                {
                    bool leggTilOK = _vyBLL.leggTilBane(bane);
                    if (leggTilOK)
                    {
                        return RedirectToAction("OversiktBaner");
                    }
                }
            }
            return View();
        }

        [SessionSjekker]
        public ActionResult LeggTilAvgang(int id)
        {
            var stasjon = _vyBLL.hentEnStasjon(id);
            var stasjonPaaBane = new stasjonPaaBane()
            {
                StasjonsID = stasjon.StasjonID,
                Stasjon = stasjon.Stasjonsnavn
            };

            return View(stasjonPaaBane);
        }

        [HttpPost]
        public ActionResult LeggTilAvgang(stasjonPaaBane stasjonPaaBane)
        {
            //Sørger for at man blir stående i samme view om man legger inn en avgang som finnes fra før
            var stasjon = _vyBLL.hentEnStasjon(stasjonPaaBane.StasjonsID);
            stasjonPaaBane.Stasjon = stasjon.Stasjonsnavn;

            if (ModelState.IsValid)
                {
                bool avgangOK = _vyBLL.sjekkAvgangOK(stasjonPaaBane);
                if (avgangOK)
                {
                    bool leggtilOK = _vyBLL.leggTilStasjonPaaBane(stasjonPaaBane.Avgang, stasjonPaaBane.StasjonsID, stasjonPaaBane.BaneID);
                    if (leggtilOK)
                    {
                        return RedirectToAction("OversiktStasjoner");
                    }
                }
            }
            return View(stasjonPaaBane);
        }

        public string hentAlleStasjoner()
        {
            List<stasjon> alleStasjoner = _vyBLL.hentAlleStasjoner();
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(alleStasjoner);
            return json;
        }

        public string hentAlleBanenavn()
        {
            List<bane> alleBaner = _vyBLL.hentAlleBaner();
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(alleBaner);
            return json;
        }
    }
}