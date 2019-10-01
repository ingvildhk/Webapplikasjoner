﻿using Oppg1.Models;
using System;
using System.Collections.Generic;

namespace Oppg1
{
    public class BestillingDB
    {
        DB db = new DB();

        public String hentStasjonsNavn(int id)
        {
            Stasjon stasjon = db.Stasjon.Find(id);
            String stasjonNavn = stasjon.Stasjonsnavn;
            return stasjonNavn;
        }

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


            //henter alle banene som inneholder FraStasjon
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

            //legger til alle stasjonene som kjører fra FraStasjon i en liste
            foreach (Bane bane in fraStasjonBaneListe)
            {
                foreach (StasjonPaaBane stasjonPaaBane in bane.StasjonPaaBane)
                {
                    //hindrer at FraStasjon blir lagt til i lista
                    if (stasjonPaaBane.Stasjon != fraStasjon)
                    {
                        if (!tilStasjoner.Contains(stasjonPaaBane.Stasjon))
                        {
                            tilStasjoner.Add(stasjonPaaBane.Stasjon);
                        }
                    }
                }
            }
            return tilStasjoner;
        }

        public List<String> hentTidspunkt(int fraStasjon, int tilStasjon, string dato)
        {
            Stasjon FraStasjon = db.Stasjon.Find(fraStasjon);
            Stasjon TilStasjon = db.Stasjon.Find(tilStasjon);

            var FraBaner = new List<Bane>();

            foreach (Bane bane in db.Bane)
            {
                foreach (StasjonPaaBane stasjonPaaBane in bane.StasjonPaaBane)
                {
                    if (stasjonPaaBane.Stasjon == FraStasjon)
                    {
                        FraBaner.Add(bane);
                    }
                }
            }

            var FraTilBaner = new List<Bane>();
            foreach (Bane bane in FraBaner)
            {
                foreach (StasjonPaaBane stasjonPaaBane in bane.StasjonPaaBane)
                {
                    if (stasjonPaaBane.Stasjon == TilStasjon)
                    {
                        FraTilBaner.Add(bane);
                    }
                }
            }

            var Avgangstider = new List<String>();

            //Finner nåværende tidspunkt og konverter til double
            String naaTidspunktString = DateTime.Now.ToString("HH:mm");
            naaTidspunktString = naaTidspunktString.Replace(':', ',');

            //sjekker om nåværende tidspunkt kan konverteres til double, ellers setter den til 0
            double naaTidspunkt;
            if (!Double.TryParse(naaTidspunktString, out naaTidspunkt))
            {
                naaTidspunkt = 0.0;
            }

            //Finner dagens dato
            DateTime dagensDato = DateTime.Now.Date;

            //Konverterer valgt dato til DateTime
            DateTime valgDato = DateTime.Parse(dato);


            //Sammenligner dagens dato med valgt dato. Valgt dato skal aldri være lavere enn dagens dato
            int sammenligning = DateTime.Compare(valgDato, dagensDato);

            //Hvis valgt dato er senere enn dagens dato, lister ut alle tidspunkt
            if (sammenligning > 0)
            {
                foreach (Bane bane in FraTilBaner)
                {
                    foreach (StasjonPaaBane stasjonPaaBane in bane.StasjonPaaBane)
                    {
                        if (stasjonPaaBane.Stasjon == FraStasjon)
                        {
                            if (!Avgangstider.Contains(stasjonPaaBane.Avgang))
                            {
                                Avgangstider.Add(stasjonPaaBane.Avgang);
                            }
                        }
                    }
                }
            }

            //Hvis dato valgt er samme som dagens dato, lister kun ut tidspunkt som ikke har vært
            if (sammenligning == 0)
            {
                foreach (Bane bane in FraTilBaner)
                {
                    foreach (StasjonPaaBane stasjonPaaBane in bane.StasjonPaaBane)
                    {
                        if (stasjonPaaBane.Stasjon == FraStasjon)
                        {
                            //Finner avgangstidene, sjekker om de kan konverteres til double og sammenligner med nåværede tidspunkt
                            String avgangsTidspunktString = stasjonPaaBane.Avgang;
                            avgangsTidspunktString = avgangsTidspunktString.Replace(':', ',');
                            double avgangsTidspunkt;
                            if (!Double.TryParse(avgangsTidspunktString, out avgangsTidspunkt))
                            {
                                avgangsTidspunkt = 0.0;
                            }
                            if (naaTidspunkt < avgangsTidspunkt)
                            {
                                if (!Avgangstider.Contains(stasjonPaaBane.Avgang))
                                {
                                    Avgangstider.Add(stasjonPaaBane.Avgang);
                                }
                            }
                        }
                    }
                }
            }
            return Avgangstider;
        }

        public List<String> hentReturTidspunkt(int fraStasjon, int tilStasjon, string dato, string returDato, string avgang)
        {
            Stasjon FraStasjon = db.Stasjon.Find(fraStasjon);
            Stasjon TilStasjon = db.Stasjon.Find(tilStasjon);

            var FraBaner = new List<Bane>();

            foreach (Bane bane in db.Bane)
            {
                foreach (StasjonPaaBane stasjonPaaBane in bane.StasjonPaaBane)
                {
                    if (stasjonPaaBane.Stasjon == FraStasjon)
                    {
                        FraBaner.Add(bane);
                    }
                }
            }

            var FraTilBaner = new List<Bane>();
            foreach (Bane bane in FraBaner)
            {
                foreach (StasjonPaaBane stasjonPaaBane in bane.StasjonPaaBane)
                {
                    if (stasjonPaaBane.Stasjon == TilStasjon)
                    {
                        FraTilBaner.Add(bane);
                    }
                }
            }

            var Avgangstider = new List<String>();

            //Konverterer avgangstidspunkt til double
            String avgangsTidspunktString = avgang;
            avgangsTidspunktString = avgangsTidspunktString.Replace(':', ',');
            double avgangstidspunkt;
            if (!Double.TryParse(avgangsTidspunktString, out avgangstidspunkt))
            {
                avgangstidspunkt = 0.0;
            }

            //Konverterer avgangsdato til Datetime
            DateTime avgangsDato = DateTime.Parse(dato);

            //Konverterer returdato til DateTime
            DateTime returdato = DateTime.Parse(returDato);


            //Sammenligner dagens dato med valgt dato. Returdato skal aldri være lavere enn avgangsdato
            int sammenligning = DateTime.Compare(returdato, avgangsDato);

            //Hvis valgt dato er senere enn dagens dato, lister ut alle tidspunkt
            if (sammenligning > 0)
            {
                foreach (Bane bane in FraTilBaner)
                {
                    foreach (StasjonPaaBane stasjonPaaBane in bane.StasjonPaaBane)
                    {
                        if (stasjonPaaBane.Stasjon == FraStasjon)
                        {
                            if (!Avgangstider.Contains(stasjonPaaBane.Avgang))
                            {
                                Avgangstider.Add(stasjonPaaBane.Avgang);
                            }
                        }
                    }
                }
            }

            //Hvis avgangsdato og returdato er like, lister kun ut tidspunkt som ikke har vært
            if (sammenligning == 0)
            {
                foreach (Bane bane in FraTilBaner)
                {
                    foreach (StasjonPaaBane stasjonPaaBane in bane.StasjonPaaBane)
                    {
                        if (stasjonPaaBane.Stasjon == FraStasjon)
                        {
                            //Finner avgangstidene, sjekker om de kan konverteres til double og sammenligner med avgangtiden
                            String returAvgangstidspunktString = stasjonPaaBane.Avgang;
                            returAvgangstidspunktString = returAvgangstidspunktString.Replace(':', ',');
                            double returAvgangstidspunkt;
                            if (!Double.TryParse(returAvgangstidspunktString, out returAvgangstidspunkt))
                            {
                                returAvgangstidspunkt = 0.0;
                            }
                            if (avgangstidspunkt < returAvgangstidspunkt)
                            {
                                if (!Avgangstider.Contains(stasjonPaaBane.Avgang))
                                {
                                    Avgangstider.Add(stasjonPaaBane.Avgang);
                                }
                            }
                        }
                    }
                }
            }
            return Avgangstider;
        }

        public bool lagreBestilling(BestillingHjelp innBestilling)
        {
            using (var db = new DB())
            {

                Bestilling bestilling = new Bestilling()
                {
                    fraStasjon = innBestilling.fraStasjon,
                    tilStasjon = innBestilling.tilStasjon,
                    dato = innBestilling.dato,
                    returDato = innBestilling.returDato,
                    avgang = innBestilling.avgang,
                    returAvgang = innBestilling.returAvgang,
                    epost = innBestilling.epost
                };

                try
                {
                    db.Bestilling.Add(bestilling);
                    db.SaveChanges();
                    return true;
                }

                catch (Exception feil)
                {
                    return false;
                }
            }
        }
    }
}