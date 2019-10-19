using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Script.Serialization;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Oppg1.Controllers
{
    public class HomeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Index()
        {
            // For å kunne ta vare på bestillingsobjektet 
            Session["Bestillingen"] = new bestilling();
            return View();
        }

        [HttpPost]
        public ActionResult Index(bestilling innBestilling)
        {
            var BLL = new VyBLL();
            Session["Bestillingen"] = innBestilling;
            
            //sjekker om fraStasjon er valgt
            if (string.IsNullOrEmpty(innBestilling.fraStasjon) || innBestilling.fraStasjon == "Velg stasjon")
            {
                log.Info("Ikke valgt 'fra' stasjon");
                ModelState.AddModelError("fraStasjon", "Velg stasjon å reise fra");
            }

            //sjekker om tilStasjon er valgt
            if (string.IsNullOrEmpty(innBestilling.tilStasjon) || innBestilling.tilStasjon == "Velg stasjon")
            {
                log.Info("Ikke valgt 'til' stasjon");
                ModelState.AddModelError("tilStasjon", "Velg stasjon å reise til ");
            }

            //sjekker om dato er valgt og på riktig format
            DateTime date;
            bool validDate = DateTime.TryParseExact(
                innBestilling.dato, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

            if (string.IsNullOrEmpty(innBestilling.dato) || innBestilling.dato == "dd.mm.åååå")
            {
                log.Info("Ikke valgt dato");
                ModelState.AddModelError("dato", "Velg dato");
            }
            else if (!validDate)
            {

                log.Info("Dato valgt er feil format");
                ModelState.AddModelError("dato", "Dato må være på korrekt format");
            }

            //sjekker om tidspunkt er valgt og på riktig format
            DateTime time;
            bool validTime = DateTime.TryParseExact(
                innBestilling.avgang, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out time);

            if (string.IsNullOrEmpty(innBestilling.avgang) || innBestilling.avgang == "Velg tidspunkt")
            {
                log.Info("Ikke valgt tidspunkt");
                ModelState.AddModelError("avgang", "Velg tidspunkt");
            }
            else if (!validTime)
            {
                log.Info("Tidspunkt i feil format");
                ModelState.AddModelError("avgang", "Tidspunkt må være på korrekt format");
            }

            //sjekker om tur/retur er valgt og kontrollerer at returdato og klokkeslett er valgt
            if (innBestilling.returKnapp == "tur/retur")
            {
                DateTime returDate;
                bool validReturDate = DateTime.TryParseExact(
                    innBestilling.returDato, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out returDate);

                if (string.IsNullOrEmpty(innBestilling.returDato) || innBestilling.returDato == "dd.mm.åååå")
                {
                    log.Info("Ikke valgt returdato");
                    ModelState.AddModelError("returDato", "Velg returdato");
                }
                else if (!validReturDate)
                {
                    log.Info("Returdato har feil format");
                    ModelState.AddModelError("returdato", "Dato må være på korrekt format");
                }

                DateTime returTime;
                bool validReturTime = DateTime.TryParseExact(
                    innBestilling.returAvgang, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out returTime);

                if (string.IsNullOrEmpty(innBestilling.returAvgang) || innBestilling.returAvgang == "Velg tidspunkt")
                {
                    log.Info("Ikke valgt returtidspunkt");
                    ModelState.AddModelError("returAvgang", "Velg returtidspunkt");
                }
                else if (!validReturTime)
                {
                    log.Info("Returtidspunkt feil format");
                    ModelState.AddModelError("returAvgang", "Tidspunkt må være på korrekt format");
                }
            }

            //hvis alt er i orden, sender videre til bestilling, ellers blir stående på siden
            if (!ModelState.IsValid)
            {
                log.Error("Feil i håndtering av bestillingsvalidering");

            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("Bestilling");
            }

            return View(innBestilling);
        }

        public ActionResult Bestilling()
        {
            var bestilling = (bestilling)Session["Bestillingen"];

            //Presenterer dato på dd.MM.yyyy format
            string[] sdato = bestilling.dato.Split('-');
            var formatertDato = sdato[2] + "." + sdato[1] + "." + sdato[0];
            bestilling.dato = formatertDato;

            if (!String.IsNullOrEmpty(bestilling.returDato))
            {
                string[] s = bestilling.returDato.Split('-');
                var formatertReturDato = s[2] + "." + s[1] + "." + s[0];
                bestilling.returDato = formatertReturDato;
            }

            return View(bestilling);
        }

        [HttpPost]
        public ActionResult Bestilling(String epost)
        {
            var bestilling = (bestilling)Session["Bestillingen"];

            //sjekker om epost er fylt ut på korrekt format
            if (string.IsNullOrEmpty(epost) || epost == "Skriv epostadresse her" || epost == "")
            {
                log.Info("Ikke skrevet inn epostadresse");
                ModelState.AddModelError("epost", "Skriv inn epostadresse");
            }

            else
            {
                try
                {
                    MailAddress m = new MailAddress(epost);
                }
                catch (FormatException)
                {
                    log.Info("Epost på feil format");
                    ModelState.AddModelError("epost", "epost må være på korrekt format");
                }
            }

            var innBestilling = new bestilling()
            {
                fraStasjon = bestilling.fraStasjon,
                tilStasjon = bestilling.tilStasjon,
                dato = bestilling.dato,
                avgang = bestilling.avgang,
                returDato = bestilling.returDato,
                returAvgang = bestilling.returAvgang,
                epost = epost
            };

            var BLL = new VyBLL();
            //sjekker at alle bestillingsdata er korrekt før lagring til databasen
            if (BLL.sjekkBestilling(innBestilling))
            {
                if (BLL.lagreBestilling(innBestilling))
                {
                    //sender bekreftelse på epost
                    try
                    {
                        var senderEmail = new MailAddress("Watchful.OsloMet@gmail.com", "VY Oppgave-1");
                        var receiverEmail = new MailAddress(epost, "Receiver");
                        var password = "ovwkmahkayjcbpxb";
                        var sub = "Bestillingsbekreftelse";
                        var body = "Takk for din bestilling!";
                        body += "\n\nFra Stasjon: " + innBestilling.fraStasjon;
                        body += "\nTil Stasjon: " + innBestilling.tilStasjon;
                        body += "\nDato: " + innBestilling.dato;
                        body += "\nKlokkeslett: " + innBestilling.avgang;
                        if (innBestilling.returAvgang != null)
                        {
                            body += "\nReturdato: " + innBestilling.returDato;
                            body += "\nReturklokkeslett: " + innBestilling.returAvgang;
                        }
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };
                        using (var mess = new MailMessage(senderEmail, receiverEmail)
                        {
                            Subject = sub,
                            Body = body
                        })
                        {
                            smtp.Send(mess);
                        }
                    }

                    catch (Exception)
                    {
                        log.Error("Kunne ikke sende bekreftelse på mail");
                        ViewBag.save = "Kunne ikke sende bekreftelse på mail";
                    }
                    return View("Bekreftelse");
                }

                else
                {
                    log.Error("Kunne ikke lagre kjøp til database");
                    ViewBag.save = "Kjøp ikke gjennomført, kunne ikke lagre til database";
                    return View(innBestilling);
                }
            }

            else
            {
                log.Error("Feil i kjøpsdata");
                ViewBag.save = "Kjøp ikke gjennomført, feil i kjøpsdata";
                return View(innBestilling);
            }
        }
        
        public string hentFraStasjoner()
        {
            var BLL = new VyBLL();
            List<String> alleStasjoner = BLL.hentAlleStasjonsNavn();
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(alleStasjoner);
            return json;
        }

        public string hentTilStasjoner(String id)
        {
            var BLL = new VyBLL();
            List<String> tilStasjoner = BLL.hentTilStasjonsNavn(id);
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(tilStasjoner);
            return json;
        }

        public string hentTidspunkt(String fraStasjon, String tilStasjon, string dato)
        {
            var BLL = new VyBLL();
            string test = dato;
            List<String> Avganger = BLL.hentTidspunkt(fraStasjon, tilStasjon, dato);
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(Avganger);
            return json;
        }

        public string hentReturTidspunkt(String fraStasjon, String tilStasjon, string dato, string returDato, string avgang)
        {
            var BLL = new VyBLL();
            List<String> Avganger = BLL.hentReturTidspunkt(fraStasjon, tilStasjon, dato, returDato, avgang);
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(Avganger);
            return json;
        }
    }
}