using System;
using System.Collections.Generic;
using System.Globalization;
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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
            if (string.IsNullOrEmpty(endreStasjon.Stasjonsnavn))
            {
                log.Warn("Stasjonsnavn må oppgis 'Endre Stasjon'");
                ModelState.AddModelError("Stasjonsnavn", "Stasjonnavn må oppgis");
            }

            if (ModelState.IsValid)
            {
                //sjekker at stasjonen ikke finnes fra før
                bool nyStasjonOK = _vyBLL.sjekkStasjonOK(endreStasjon);
                if (nyStasjonOK)
                {
                    bool endringOK = _vyBLL.endreStasjon(id, endreStasjon);
                    if (endringOK)
                    {
                        log.Info("Endring på Stasjon utført! Nytt navn er: " + endreStasjon.Stasjonsnavn);
                        return RedirectToAction("OversiktStasjoner");
                    }
                }
                else
                {
                    log.Warn("Stasjon finnes fra før 'Endre Stasjon'");
                    ModelState.AddModelError("Stasjonsnavn", "Stasjonen finnes fra før");
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
            if (string.IsNullOrEmpty(endreBane.Banenavn))
            {
                log.Warn("Banenavn er tom, oppgi banenavn 'Endre Bane'");
                ModelState.AddModelError("Banenavn", "Banenavn må oppgis");
            }

            if (ModelState.IsValid)
            {
                //sjekker at bane ikke finnes fra før
                bool nyBaneOK = _vyBLL.sjekkBaneOK(endreBane);
                if (nyBaneOK)
                {
                    bool endringOK = _vyBLL.endreBane(id, endreBane);
                    if (endringOK)
                    {

                        log.Info("Endring på Bane utført! Nytt navn er: " + endreBane.Banenavn);
                        return RedirectToAction("OversiktBaner");
                    }
                }
                else
                {
                    log.Warn("Banenavn finnes fra før 'Endre Bane'");
                    ModelState.AddModelError("Banenavn", "Banen finnes fra før");
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
            if (string.IsNullOrEmpty(endreStasjonPaaBane.Avgang))
            {
                log.Warn("Oppgi tidspunkt for 'Endre Avgang'");
                ModelState.AddModelError("Avgang", "Tidspunkt må oppgis");
            }

            //sjekker om tidspunkt er valgt og på riktig format
            var metodeSjekk = new ValideringsMetoder();
            bool tidspunktOk = metodeSjekk.sjekkTidspunkt(endreStasjonPaaBane.Avgang);
            if (!tidspunktOk)
            {
                log.Warn("Tidspunkt må ha korrekt format - 'Endre Agang'");
                ModelState.AddModelError("Avgang", "Tidspunkt må være på korrekt format");
            }

            if (ModelState.IsValid)
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
                        log.Info("Endring på Avgang utført! Nytt navn er: " + endreStasjonPaaBane.Avgang);
                        //må endre denne til oversikt over avgang på stasjon
                        return RedirectToAction("OversiktStasjoner");
                    }
                }
                else
                {
                    log.Warn("Avgang finnes fra før 'Endre Avgang'");
                    ModelState.AddModelError("Avgang", "Avgangen finnes fra før");
                }
            }
            return View(endreStasjonPaaBane);
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
            stasjon s = _vyBLL.hentEnStasjon(id);
            String Stasjonsnavn = s.Stasjonsnavn;
            bool slettOK = _vyBLL.slettStasjon(id);
            if (slettOK)
            {
                log.Info("Sletting av stasjon "+ Stasjonsnavn + " var vellykket!");
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
                log.Info("Sletting av bane var vellykket!");
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
                log.Info("Sletting av avgang: " + avgang + "var vellykket!");
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
            if (string.IsNullOrEmpty(stasjon.Stasjonsnavn))
            {
                log.Warn("Stasjonsnavn må oppgis 'Legg til Stasjon'");
                ModelState.AddModelError("Stasjonsnavn", "Stasjonnavn må oppgis");
            }

            if (ModelState.IsValid)
            {
                //sjekker at stasjon ikke finnes fra før
                bool nyStasjonOK = _vyBLL.sjekkStasjonOK(stasjon);
                if (nyStasjonOK)
                {
                    bool leggTilOK = _vyBLL.leggTilStasjon(stasjon);
                    if (leggTilOK)
                    {
                        log.Info("Ny stasjon lagt til: " + stasjon.Stasjonsnavn);
                        return RedirectToAction("OversiktStasjoner");
                    }
                }
                else
                {
                    log.Warn("Stasjon finnes fra før 'Legg til Stasjon'");
                    ModelState.AddModelError("Stasjonsnavn", "Stasjonen finnes fra før");
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
            if (string.IsNullOrEmpty(bane.Banenavn))
            {
                log.Warn("Oppgi Banenavn 'Legg til Bane'");
                ModelState.AddModelError("Banenavn", "Banenavn må oppgis");
            }

            if (ModelState.IsValid)
            {
                //sjekker at banen ikke finnes fra før
                bool nyBaneOK = _vyBLL.sjekkBaneOK(bane);
                if (nyBaneOK)
                {
                    bool leggTilOK = _vyBLL.leggTilBane(bane);
                    if (leggTilOK)
                    {
                        log.Info("Ny Bane lagt til: " + bane.Banenavn);
                        return RedirectToAction("OversiktBaner");
                    }
                }
                else
                {
                    log.Warn("Bane finnes fra før 'Legg til Bane'");
                    ModelState.AddModelError("Banenavn", "Banen finnes fra før");
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

            if (string.IsNullOrEmpty(stasjonPaaBane.Avgang))
            {
                log.Warn("Oppgi tidspunkt 'Legg til Avgang'");
                ModelState.AddModelError("Avgang", "Tidspunkt må oppgis");
            }

            //sjekker om tidspunkt er valgt og på riktig format
            var metodeSjekk = new ValideringsMetoder();
            bool tidspunktOk = metodeSjekk.sjekkTidspunkt(stasjonPaaBane.Avgang);
            if (!tidspunktOk)
            {
                log.Warn("Oppgi tidspunkt i rett format 'Legg til Avgang'");
                ModelState.AddModelError("Avgang", "Tidspunkt må være på korrekt format");
            }

            if (string.IsNullOrEmpty(stasjonPaaBane.Bane) || stasjonPaaBane.Bane == "Velg Bane")
            {
                log.Warn("feil ved - Velg Bane 'Legg til Avgang'");
                ModelState.AddModelError("Avgang", "Velg Bane");
            }

            if (ModelState.IsValid)
                {
                //sjekker om avgangen finnes fra før
                bool avgangOK = _vyBLL.sjekkAvgangOK(stasjonPaaBane);
                if (avgangOK)
                {
                    bool leggtilOK = _vyBLL.leggTilStasjonPaaBane(stasjonPaaBane.Avgang, stasjonPaaBane.StasjonsID, stasjonPaaBane.BaneID);
                    if (leggtilOK)
                    {

                        log.Info("Ny avgang lagt til på stasjon: " + stasjonPaaBane.Bane + " tidspunkt: " + stasjonPaaBane.Avgang);
                        return RedirectToAction("OversiktStasjoner");
                    }
                }
                else
                {
                    log.Warn("Avgang finnes fra før 'Legg til Avgang'");
                    ModelState.AddModelError("Avgang", "Avgangen finnes fra før");
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