using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oppg1.Models;

namespace Oppg1
{
    public class BestillingDB
    {
        DB db = new DB();

        public List<stasjon> hentAlleStasjoner()
        {
            List<stasjon> alleStasjoner = db.Stasjon.Select(s => new stasjon()
            {
                stasjonsID = s.stasjonsID,
                Stasjonsnavn = s.Stasjonsnavn
            }).ToList();
            return alleStasjoner;
        }

        public List<stasjon> hentTilStasjoner(int id)
        {
            List<Bane> alleBaner = new List<Bane>();

            foreach (Bane bane in db.Bane)
            {
                foreach (Stasjon stasjon in bane.Stasjoner)
                {
                    if (stasjon.stasjonsID == id)
                    {
                        alleBaner.Add(bane);
                    }
                }
            }

            List<Stasjon> TilStasjoner = new List<Stasjon>();

            foreach (Bane bane in alleBaner)
            {
                foreach (Stasjon stasjon in bane.Stasjoner)
                {
                    if (!TilStasjoner.Contains(stasjon))
                    {
                        TilStasjoner.Add(stasjon);
                    }
                }
            }

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
            return tilStasjoner;
        }
    }
}