using Oppg1.Models;
using System;
using System.Collections.Generic;
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

            return RedirectToAction("Bestilling");
        }

        public ActionResult Bestilling()
        {
            var bestilling = (BestillingHjelp)Session["Bestillingen"];
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

            try
            {
                var senderEmail = new MailAddress("Watchful.OsloMet@gmail.com", "VY Oppgave1");
                var receiverEmail = new MailAddress(epost, "Receiver");
                var password = "ovwkmahkayjcbpxb";
                var sub = "Bestillingsbekreftelse";
                var body = "Takk for din bestilling!";
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
                ViewBag.Error = "Some Error";
            }

            var Bdb = new BestillingDB();
            if (Bdb.lagreBestilling(innBestilling))
            {
                return View("Bekreftelse");
            }
            else
            {
                // Prøve å returnere noe fornuftig her :):):)
                return ViewBag("Hei");
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