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


            //skal hente alle banene som inneholder FraStasjon
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

            //skal legge til alle stasjonene som kjører fra FraStasjon i en liste
            foreach (Bane bane in fraStasjonBaneListe)
            {
                foreach (StasjonPaaBane stasjonPaaBane in bane.StasjonPaaBane)
                {
                    //skal hindre at FraStasjon blir lagt til i lista
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
            double naaTidspunkt = Convert.ToDouble(naaTidspunktString);

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
                            //Finner avgangstidene, konverterer til double og sammenligner med nåværede tidspunkt
                            String avgangsTidspunktString = stasjonPaaBane.Avgang;
                            avgangsTidspunktString = avgangsTidspunktString.Replace(':', ',');
                            double avgangsTidspunkt = Convert.ToDouble(avgangsTidspunktString);

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
    }
}