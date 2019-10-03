﻿using Oppg1.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Oppg1.Controllers
{
    public class HomeController : Controller
    {
        private DB db = new DB();

        public ActionResult Index()
        {
            // For å kunne ta vare på bestillingsobjektet 
            Session["Bestillingen"] = new BestillingHjelp();
            return View();
        }

        [HttpPost]
        public ActionResult Index(BestillingHjelp innBestilling)
        {
            var Bdb = new BestillingDB();
            Session["Bestillingen"] = innBestilling;

            if (string.IsNullOrEmpty(innBestilling.fraStasjon) || innBestilling.fraStasjon == "Velg stasjon")
            {
                ModelState.AddModelError("fraStasjon", "Velg stasjon å reise fra");
            }

            if (string.IsNullOrEmpty(innBestilling.tilStasjon) || innBestilling.tilStasjon == "Velg stasjon")
            {
                ModelState.AddModelError("tilStasjon", "Velg stasjon å reise til ");
            }

            DateTime date;
            bool validDate = DateTime.TryParseExact(
                innBestilling.dato, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

            if (string.IsNullOrEmpty(innBestilling.dato) || innBestilling.dato == "dd.mm.åååå")
            {
                ModelState.AddModelError("dato", "Velg dato");
            }
            else if (!validDate)
            {
                ModelState.AddModelError("dato", "Dato må være på korrekt format");
            }

            DateTime time;
            bool validTime = DateTime.TryParseExact(
                innBestilling.avgang, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out time);

            if (string.IsNullOrEmpty(innBestilling.avgang) || innBestilling.avgang == "Velg tidspunkt")
            {
                ModelState.AddModelError("avgang", "Velg tidspunkt");
            }
            else if (!validTime)
            {
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
                    ModelState.AddModelError("returDato", "Velg returdato");
                }
                else if (!validReturDate)
                {
                    ModelState.AddModelError("returdato", "Dato må være på korrekt format");
                }

                DateTime returTime;
                bool validReturTime = DateTime.TryParseExact(
                    innBestilling.returAvgang, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out returTime);

                if (string.IsNullOrEmpty(innBestilling.returAvgang) || innBestilling.returAvgang == "Velg tidspunkt")
                {
                    ModelState.AddModelError("returAvgang", "Velg returtidspunkt");
                }
                else if (!validReturTime)
                {
                    ModelState.AddModelError("returAvgang", "Tidspunkt må være på korrekt format");
                }
            }
            //Sender videre stasjonsnavn og ikke stasjonsindeks, setter id til 1 om parsing ikke virker
            int fraId;
            if (!int.TryParse(innBestilling.fraStasjon, out fraId))
            {
                fraId = 1;
            }
            String fraStasjon = Bdb.hentStasjonsNavn(fraId);
            innBestilling.fraStasjon = fraStasjon;

            int tilId;
            if (!int.TryParse(innBestilling.tilStasjon, out tilId))
            {
                tilId = 1;
            }
            String tilStasjon = Bdb.hentStasjonsNavn(tilId);
            innBestilling.tilStasjon = tilStasjon;

            if (ModelState.IsValid)
            {
                return RedirectToAction("Bestilling");
            }
            return View(innBestilling);
        }

        public ActionResult Bestilling()
        {
            var bestilling = (BestillingHjelp)Session["Bestillingen"];

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
            var bestilling = (BestillingHjelp)Session["Bestillingen"];

            if (string.IsNullOrEmpty(epost) || epost == "Skriv epostadresse her" || epost == "")
            {
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
                    ModelState.AddModelError("epost", "epost må være på korrekt format");
                }
            }

            var innBestilling = new BestillingHjelp()
            {
                fraStasjon = bestilling.fraStasjon,
                tilStasjon = bestilling.tilStasjon,
                dato = bestilling.dato,
                avgang = bestilling.avgang,
                returDato = bestilling.returDato,
                returAvgang = bestilling.returAvgang,
                epost = epost
            };

            var Bdb = new BestillingDB();
            if (Bdb.sjekkBestilling(innBestilling))
            {
                if (Bdb.lagreBestilling(innBestilling))
                {
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
                        ViewBag.save = "Kunne ikke sende bekreftelse på mail";
                    }
                    return View("Bekreftelse");
                }

                else
                {
                    ViewBag.save = "Kjøp ikke gjennomført, kunne ikke lagre til database";
                    return View(innBestilling);
                }
            }

            else
            {
                ViewBag.save = "Kjøp ikke gjennomført, feil i kjøpsdata";
                return View(innBestilling);
            }
        }
        
        public string hentFraStasjoner()
        {
            var Bdb = new BestillingDB();
            List<Stasjon> alleStasjoner = Bdb.hentAlleStasjoner();
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(alleStasjoner);
            return json;
        }

        public string hentTilStasjoner(int id)
        {
            var Bdb = new BestillingDB();
            List<Stasjon> tilStasjoner = Bdb.hentTilStasjoner(id);
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(tilStasjoner);
            return json;
        }

        public string hentTidspunkt(int fraStasjon, int tilStasjon, string dato)
        {
            var Bdb = new BestillingDB();
            string test = dato;
            List<String> Avganger = Bdb.hentTidspunkt(fraStasjon, tilStasjon, dato);
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(Avganger);
            return json;
        }

        public string hentReturTidspunkt(int fraStasjon, int tilStasjon, string dato, string returDato, string avgang)
        {
            var Bdb = new BestillingDB();
            string test = dato;
            List<String> Avganger = Bdb.hentReturTidspunkt(fraStasjon, tilStasjon, dato, returDato, avgang);
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(Avganger);
            return json;
        }
    }
}