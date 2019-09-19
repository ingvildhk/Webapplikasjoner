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

        public List<Avgangstider>hentTidspunkt(int fraStasjon, int tilStasjon)
        {

            Stasjon FraStasjon = db.Stasjon.Find(fraStasjon);
            Stasjon TilStasjon = db.Stasjon.Find(tilStasjon);


            List<Bane> fraBaner = new List<Bane>();

            foreach (Bane bane in db.Bane)
            {
                foreach (Stasjon stasjon in bane.Stasjoner)
                {
                    if (stasjon.stasjonsID == fraStasjon)
                    {
                        fraBaner.Add(bane);
                    }
                }
            }

            //Liste med baner som kjører igjennom begge stasjonene
            List<Bane> fraOgTilBaner = new List<Bane>();

            foreach(Bane bane in fraBaner)
            {
                foreach (Stasjon stasjon in bane.Stasjoner)
                {
                    if (stasjon.stasjonsID == tilStasjon)
                    {
                        fraOgTilBaner.Add(bane);
                    }

                }
            }

            //Finne baner som kjører igjennom begge stasjonene i riktig rekkefølge
            List<Bane> iRikigRekkefølge = new List<Bane>();

            foreach(Bane bane in fraOgTilBaner)
            {
                int fraIndex = bane.Stasjoner.IndexOf(FraStasjon);
                int tilIndex = bane.Stasjoner.IndexOf(TilStasjon);
                Console.WriteLine(fraIndex);
                Console.WriteLine(tilIndex);
                if (fraIndex < tilIndex)
                {
                    iRikigRekkefølge.Add(bane);
                }
            }

            List<Avgangstider> Avgangstider = new List<Avgangstider>();

            foreach (Bane bane in iRikigRekkefølge)
            {
                foreach(Stasjon stasjon in bane.Stasjoner)
                {
                    if (stasjon == FraStasjon)
                    {
                        foreach (Avganger avganger in stasjon.Avganger)
                        {
                            if (avganger.Bane == bane)
                            {
                                foreach (Avgangstider avgangstider in avganger.Avgangstider)
                                {
                                    Avgangstider.Add(avgangstider);
                                }
                            }
                        }

                    }
                }
            }
            return Avgangstider;
        }
    }
}