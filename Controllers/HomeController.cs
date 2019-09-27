using Oppg1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Session["Bestillingen"] = new List<Models.TestBestilling>();
            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.TestBestilling innBestilling)
        {
            var bestillingsliste = (List<Models.TestBestilling>)Session["Bestillingen"];
            bestillingsliste.Add(innBestilling);
            Session["Bestillingen"] = bestillingsliste;
            return RedirectToAction("Bestilling");
        }

        public ActionResult Bestilling()
        {
            var bestillingsliste = (List<Models.TestBestilling>)Session["Bestillingen"];
            return View(bestillingsliste);
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
    }
}