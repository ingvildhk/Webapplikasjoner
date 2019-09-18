using Oppg1.Models;
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
            List<Stasjon> TilStasjoner = Bdb.hentTilStasjoner(id);
            Stasjon FraStasjon = TilStasjoner.Find(s => s.stasjonsID == id);
            TilStasjoner.Remove(FraStasjon);

            List<stasjon> tilStasjoner = new List<stasjon>();

            foreach (Stasjon tilStasjon in TilStasjoner)
            {
                stasjon s = new stasjon();
                s.stasjonsID = tilStasjon.stasjonsID;
                s.Stasjonsnavn = tilStasjon.Stasjonsnavn;
                tilStasjoner.Add(s);
            }
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(tilStasjoner);
            return json;
        }
    }
}