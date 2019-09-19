﻿using Oppg1.Models;
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
            return View();
        }

        public string hentFraStasjoner()
        {
            var Bdb = new BestillingDB();
            List<stasjon> alleStasjoner = Bdb.hentAlleStasjoner();
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(alleStasjoner);
            return json;
        }

        public string hentTilStasjoner(int id)
        {
            var Bdb = new BestillingDB();
            List<stasjon> tilStasjoner = Bdb.hentTilStasjoner(id);
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(tilStasjoner);
            return json;
        }

        public string hentTidspunkt(int fraStasjon, int tilStasjon)
        {
            var Bdb = new BestillingDB();
            List<Avgangstider> Avgangstider = Bdb.hentTidspunkt(fraStasjon, tilStasjon);
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(Avgangstider);
            return json;
        }
    }
}