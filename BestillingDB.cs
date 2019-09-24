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

        public List<Stasjon> hentAlleStasjoner()
        {
            List<Stasjon> alleStasjoner = new List<Stasjon>();

            foreach (Stasjon stasjon in db.Stasjon)
            {
                alleStasjoner.Add(stasjon);
            }
            return alleStasjoner;
        }

        public List<Stasjon> hentTilStasjoner(int id)
        {
            Stasjon fraStasjon = db.Stasjon.Find(id);

            var fraStasjonBaneListe = new List<Bane>();

            foreach (Bane bane in db.Bane)
            {
                foreach (StasjonPaaBane stasjonPaaBane in bane.StasjonPaaBane)
                {
                    if (stasjonPaaBane.Stasjon == fraStasjon)
                    {
                        fraStasjonBaneListe.Add(bane);
                    }
                }
            }

            var tilStasjoner = new List<Stasjon>();

            foreach (Bane bane in fraStasjonBaneListe)
            {
                foreach (StasjonPaaBane stasjonPaaBane in bane.StasjonPaaBane)
                {
                    if (stasjonPaaBane.Stasjon != fraStasjon)
                    {
                        tilStasjoner.Add(stasjonPaaBane.Stasjon);
                    }
                }
            }
           tilStasjoner = tilStasjoner.Distinct().ToList();
            return tilStasjoner;
        }

        public List<TimeSpan> hentTidspunkt(int fraStasjon, int tilStasjon)
        {
            Stasjon FraStasjon = db.Stasjon.Find(fraStasjon);
            Stasjon TilStasjon = db.Stasjon.Find(tilStasjon);

            var TilFraBaner = new List<Bane>();

            foreach (Bane bane in db.Bane)
            {
                foreach (StasjonPaaBane stasjonPaaBane in bane.StasjonPaaBane)
                {
                    if (stasjonPaaBane.Stasjon == FraStasjon)
                    {
                        foreach (StasjonPaaBane stasjonPaaBane1 in bane.StasjonPaaBane)
                        {
                            if (stasjonPaaBane1.Stasjon == TilStasjon)
                            {
                                TilFraBaner.Add(bane);
                            }
                        }
                    }
                }
            }

            var Avgangstider = new List<TimeSpan>();

            foreach (Bane bane in TilFraBaner)
            {
                foreach (StasjonPaaBane stasjonPaaBane in bane.StasjonPaaBane)
                {
                    if (stasjonPaaBane.Stasjon == FraStasjon)
                    {
                        foreach (TimeSpan timeSpan in stasjonPaaBane.Avgang)
                        {
                            Avgangstider.Add(timeSpan);
                        }
                    }
                }
            }

            return Avgangstider;

        }
    }
}