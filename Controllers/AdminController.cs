using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
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
            alleStasjoner = alleStasjoner.OrderBy(a => a.Stasjonsnavn).ToList();
            return View(alleStasjoner);
        }

        [SessionSjekker]
        public ActionResult OversiktBaner()
        {
            List<bane> alleBaner = _vyBLL.hentAlleBaner();
            alleBaner = alleBaner.OrderBy(a => a.Banenavn).ToList();
            return View(alleBaner);
        }

        [SessionSjekker]
        //Oversikt avganger til stasjoner
        public ActionResult AvgangerPaStasjon(int id)
        {
            var ID = id;
            List<stasjonPaaBane> avgangerPaaStasjon = _vyBLL.hentStasjonPaaBane(id);
            avgangerPaaStasjon = avgangerPaaStasjon.OrderBy(a => a.Bane).ThenBy(a => a.Avgang).ToList();
            if (avgangerPaaStasjon.Count == 0)
            {
                return RedirectToAction(actionName: "AvgangerPaStasjonTom", routeValues: new { id = ID });
            }
            return View(avgangerPaaStasjon);
        }

        [SessionSjekker]
        public ActionResult AvgangerPaStasjonTom(int id)
        {
            stasjon stasjon = _vyBLL.hentEnStasjon(id);
            return View(stasjon);
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
            Regex regex = new Regex("[^ÆØÅæøåa-zA-Z0-9 ]+");

            if (string.IsNullOrEmpty(endreStasjon.Stasjonsnavn))
            {
                ModelState.AddModelError("Stasjonsnavn", "Stasjonnavn må oppgis");
            }
            
            else if (regex.IsMatch(endreStasjon.Stasjonsnavn))
            {
                ModelState.AddModelError("Stasjonsnavn", "Stasjonnavn kan ikke inneholde spesialtegn");
            }

            if (ModelState.IsValid)
            {
                stasjon s = _vyBLL.hentEnStasjon(id);
                String Stasjonsnavn = s.Stasjonsnavn;

                //sjekker at stasjonen ikke finnes fra før
                bool nyStasjonOK = _vyBLL.sjekkStasjonOK(endreStasjon);
                if (nyStasjonOK)
                {
                    bool endringOK = _vyBLL.endreStasjon(id, endreStasjon);
                    if (endringOK)
                    {
                        log.Info("Stasjon " + Stasjonsnavn + " endret navn til " + endreStasjon.Stasjonsnavn + "!");
                        return RedirectToAction("OversiktStasjoner");
                    }
                    else
                    {
                        log.Error("Stasjon " + Stasjonsnavn + " ble forsøkt endret til " + endreStasjon.Stasjonsnavn + ". Endring feilet, transaksjon ble avbrutt!");
                        ViewBag.save = "Kunne ikke oppdatere stasjon";
                    }
                }
                else
                {
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
            Regex regex = new Regex("[^ÆØÅæøåa-zA-Z0-9 ]+");

            if (string.IsNullOrEmpty(endreBane.Banenavn))
            {
                ModelState.AddModelError("Banenavn", "Banenavn må oppgis");
            }

            else if (regex.IsMatch(endreBane.Banenavn))
            {
                ModelState.AddModelError("Banenavn", "Banenavn kan ikke inneholde spesialtegn");
            }

            if (ModelState.IsValid)
            {
                bane gammelBane = _vyBLL.hentEnBane(id);
                string Bane = gammelBane.Banenavn;

                //sjekker at bane ikke finnes fra før
                bool nyBaneOK = _vyBLL.sjekkBaneOK(endreBane);
                if (nyBaneOK)
                {
                    bool endringOK = _vyBLL.endreBane(id, endreBane);
                    if (endringOK)
                    {
                        log.Info("Bane " + Bane + " ble endret til " + endreBane.Banenavn + ". Endring lagret til databasen!");
                        return RedirectToAction("OversiktBaner");
                    }

                    else
                    {
                        log.Error("Bane " + Bane + " ble prøvd endret til " + endreBane.Banenavn + ". Endring feilet.Transaksjon avbrutt!");
                        ViewBag.save = "Kunne ikke oppdatere bane";
                    }
                }
                else
                {
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
                ModelState.AddModelError("Avgang", "Tidspunkt må oppgis");
            }

            //sjekker om tidspunkt er valgt og på riktig format
            var metodeSjekk = new ValideringsMetoder();
            bool tidspunktOk = metodeSjekk.sjekkTidspunkt(endreStasjonPaaBane.Avgang);
            if (!tidspunktOk)
            {
                ModelState.AddModelError("Avgang", "Tidspunkt må være på korrekt format");
            }

            if (ModelState.IsValid)
            {
                var s = _vyBLL.hentEnAvgang(id);
                string Stasjonsnavn = s.Stasjon;

                var gammelAvgang = _vyBLL.hentEnAvgang(id);
                string avg = gammelAvgang.Avgang;

                var bane = _vyBLL.hentEnBane(endreStasjonPaaBane.BaneID);
                endreStasjonPaaBane.Bane = bane.Banenavn;
                //sjekker at avgangen ikke finnes fra før 
                bool nyAvgangOK = _vyBLL.sjekkAvgangOK(endreStasjonPaaBane);
                if (nyAvgangOK)
                {
                    bool endringOK = _vyBLL.endreStasjonPaaBane(endreStasjonPaaBane, id);
                    if (endringOK)
                    {
                        log.Info("Endring på avgang " + avg + " fra stasjon " + Stasjonsnavn + " registrert. Ny avgangstid er " + endreStasjonPaaBane.Avgang + "!");
                        return RedirectToAction("AvgangerPaStasjon", "Admin", new { id = endreStasjonPaaBane.StasjonsID });
                    }

                    else
                    {
                        log.Error("Endring på avgang " + avg + " fra stasjon " + Stasjonsnavn + " feilet. Tid prøvd lagret var " + endreStasjonPaaBane.Avgang + ". Transaksjon avbrutt!");
                        ViewBag.save = "Kunne ikke oppdatere avgang";
                    }
                }
                else
                {
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
                log.Info("Sletting av stasjon " + Stasjonsnavn + " var vellykket!");
                return RedirectToAction("OversiktStasjoner");
            }
            else
            {
                log.Error("Sletting av stasjon "+ Stasjonsnavn + " feilet. Transakjson avbrutt!");
                ViewBag.save = "Kunne ikke slette stasjon";
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
            enBane = _vyBLL.hentEnBane(id);
            String Banenavn = enBane.Banenavn;

            bool slettOK = _vyBLL.slettBane(id);
            if (slettOK)
            {
                log.Info("Sletting av Bane " + Banenavn + " ble utført!");
                return RedirectToAction("OversiktBaner");
            }

            else
            {
                log.Error("Sletting av Bane " + Banenavn + " ble ikke utført. Transaksjon avbrutt!");
                ViewBag.save = "Kunne ikke slette bane";
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
            var avg  = _vyBLL.hentEnAvgang(id);
            String Avgangstid = avg.Avgang;

            var gammelAvgSta = _vyBLL.hentEnAvgang(id);
            String GammelAvg = gammelAvgSta.Stasjon;


            var baneidTilAvgang = _vyBLL.hentEnAvgang(id);
            // For å få tak i StasjonsId i RedirectToAction
            var stasjonid = baneidTilAvgang.StasjonsID;
            bool slettOK = _vyBLL.slettStasjonPaaBane(id, baneidTilAvgang.BaneID);
            if (slettOK)
            {
                log.Info("Avgang " + Avgangstid + " fra stasjon " + GammelAvg + " ble slettet!");
                List<stasjonPaaBane> avgangerPaaStasjon = _vyBLL.hentStasjonPaaBane(stasjonid);
                if (avgangerPaaStasjon.Count == 0)
                {
                    return RedirectToAction(actionName: "AvgangerPaStasjonTom", routeValues: new { id = stasjonid });
                }
                return RedirectToAction("AvgangerPaStasjon", "Admin", new { id = stasjonid });
            }
            else
            {
                log.Error("Avgang " + Avgangstid + " fra stasjon " + GammelAvg + " ble ikke slettet. Transaksjon avbrutt!");
                ViewBag.save = "Kunne ikke slette avgang";
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
            Regex regex = new Regex("[^ÆØÅæøåa-zA-Z0-9 ]+");

            if (string.IsNullOrEmpty(stasjon.Stasjonsnavn))
            {
                ModelState.AddModelError("Stasjonsnavn", "Stasjonnavn må oppgis");
            }

            else if (regex.IsMatch(stasjon.Stasjonsnavn))
            {
                ModelState.AddModelError("Stasjonsnavn", "Stasjonnavn kan ikke inneholde spesialtegn");
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
                        log.Info("Ny stasjon med navn " + stasjon.Stasjonsnavn + " lagt til i databasen");
                        return RedirectToAction("OversiktStasjoner");
                    }
                    else
                    {
                        log.Error("Ny stasjon med navn " + stasjon.Stasjonsnavn + " kunne ikke lagres. Transaksjon avbrutt!");
                        ViewBag.save = "Kunne ikke legge til stasjon";
                    }
                }
                else
                {
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
            Regex regex = new Regex("[^ÆØÅæøåa-zA-Z0-9 ]+");

            if (string.IsNullOrEmpty(bane.Banenavn))
            {
                ModelState.AddModelError("Banenavn", "Banenavn må oppgis");
            }

            else if (regex.IsMatch(bane.Banenavn))
            {
                ModelState.AddModelError("Banenavn", "Banenavn kan ikke inneholde spesialtegn");
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
                        log.Info("Ny Bane ved navn " + bane.Banenavn + " lagt til i databasen");
                        return RedirectToAction("OversiktBaner");
                    }
                    else
                    {
                        log.Error("Ny Bane med navn " + bane.Banenavn + " kunne ikke lagres. Transaksjon avbrutt!");
                        ViewBag.save = "Kunne ikke legge til bane";
                    }
                }
                else
                {
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

            //sjekker om tidspunkt er valgt og på riktig format
            var metodeSjekk = new ValideringsMetoder();
            bool tidspunktOk = metodeSjekk.sjekkTidspunkt(stasjonPaaBane.Avgang);

            if (stasjonPaaBane.BaneID == 0)
            {
                ModelState.AddModelError("BaneID", "Velg bane");
            }

            else if (string.IsNullOrEmpty(stasjonPaaBane.Avgang))
            {
                ModelState.AddModelError("Avgang", "Tidspunkt må oppgis");
            }

            else if (!tidspunktOk)
            {
                ModelState.AddModelError("Avgang", "Tidspunkt må være på korrekt format");
            }

            else if (ModelState.IsValid)
            {
                //sjekker om avgangen finnes fra før
                bool avgangOK = _vyBLL.sjekkAvgangOK(stasjonPaaBane);
                if (avgangOK)
                {
                    stasjon s = _vyBLL.hentEnStasjon(stasjonPaaBane.StasjonsID);
                    String Stasjonsnavn = s.Stasjonsnavn;

                    bool leggtilOK = _vyBLL.leggTilStasjonPaaBane(stasjonPaaBane.Avgang, stasjonPaaBane.StasjonsID, stasjonPaaBane.BaneID);
                    if (leggtilOK)
                    {

                        log.Info("Avgang " + stasjonPaaBane.Avgang + " er lagt til stasjon " + Stasjonsnavn + "!");
                        return RedirectToAction("AvgangerPaStasjon", "Admin", new { id = stasjonPaaBane.StasjonsID });
                    }

                    else
                    {
                        log.Error("Avgang " + stasjonPaaBane.Avgang + " på stasjon " + Stasjonsnavn + " ble forsøkt lagret, feil oppsto. Transaksjon avbrutt!");
                        ViewBag.save = "Kunne ikke legge til avgang";
                    }
                }
                else
                {
                    log.Debug("Kommer inn i ModelstateValid 'Legg til Avgang', men ikke videre!");
                    ModelState.AddModelError("Avgang", "Avgangen finnes fra før");
                }
            }
            log.Debug("Kommer ikke inn i ModelstateValid 'Legg til Avgang'");
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