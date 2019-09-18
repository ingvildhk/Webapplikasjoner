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

        public List<Stasjon> hentTilStasjoner(int id)
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

            List<Stasjon> tilStasjoner = new List<Stasjon>();

            foreach (Bane bane in alleBaner)
            {
                foreach (Stasjon stasjon in bane.Stasjoner)
                {
                    if (!tilStasjoner.Contains(stasjon))
                    {
                        tilStasjoner.Add(stasjon);
                    }
                }
            }
            return tilStasjoner;
        }
    }
}