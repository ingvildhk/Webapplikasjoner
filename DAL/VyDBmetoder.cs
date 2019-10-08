using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using Model;

namespace DAL
{
    public class VyDBmetoder
    {
        DB db = new DB();

        // -----------------------------------------------------------------------------------------
        //Metoder for å hente stasjonsnavn, tidspunkt o.l. til bestillingsview

        public String hentStasjonsNavn(int id)
        {
            Stasjon stasjon = db.Stasjon.Find(id);
            String stasjonNavn = stasjon.Stasjonsnavn;
            return stasjonNavn;
        }

        public Stasjon hentStasjon(String Stasjonsnavn)
        {
            Stasjon stasjon = new Stasjon();
            foreach (Stasjon s in db.Stasjon)
            {
                if (s.Stasjonsnavn == Stasjonsnavn)
                {
                    stasjon = s;
                }
            }
            return stasjon;
        }

        public List<String> hentalleStasjonsNavn()
        {
            List<String> alleStasjoner = new List<String>();

            foreach (Stasjon stasjon in db.Stasjon)
            {
                alleStasjoner.Add(stasjon.Stasjonsnavn);
            }
            return alleStasjoner;
        }

        public List<String> hentTilStasjonsNavn(String fraStasjonNavn)
        {
            Stasjon fraStasjon = hentStasjon(fraStasjonNavn);

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

            var tilStasjoner = new List<String>();

            //legger til alle stasjonene som kjører fra FraStasjon i en liste
            foreach (Bane bane in fraStasjonBaneListe)
            {
                foreach (StasjonPaaBane stasjonPaaBane in bane.StasjonPaaBane)
                {
                    //hindrer at FraStasjon blir lagt til i lista
                    if (stasjonPaaBane.Stasjon != fraStasjon)
                    {
                        if (!tilStasjoner.Contains(stasjonPaaBane.Stasjon.Stasjonsnavn))
                        {
                            tilStasjoner.Add(stasjonPaaBane.Stasjon.Stasjonsnavn);
                        }
                    }
                }
            }
            return tilStasjoner;
        }

        public List<String> hentTidspunkt(String fraStasjon, String tilStasjon, String dato)
        {
            Stasjon FraStasjon = hentStasjon(fraStasjon);
            Stasjon TilStasjon = hentStasjon(tilStasjon);

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

        public List<String> hentReturTidspunkt(String fraStasjon, String tilStasjon, string dato, string returDato, string avgang)
        {
            Stasjon FraStasjon = hentStasjon(fraStasjon);
            Stasjon TilStasjon = hentStasjon(tilStasjon);

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

            //Hvis avgangsdato og returdato er like, lister kun ut tidspunkt som er senere enn avgangstiden
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

        // --------------------------------------------------------------------------------
        //Metoder for å sjekke bestilling og lagre bestilling til database

        public bool sjekkBestilling(bestilling innBestilling)
        {
            var fraStasjon = innBestilling.fraStasjon;
            var tilStasjon = innBestilling.tilStasjon;
            var dato = innBestilling.dato;
            var returDato = innBestilling.returDato;
            var avgang = innBestilling.avgang;
            var returAvgang = innBestilling.returAvgang;
            var epost = innBestilling.epost;

            List<String> alleStasjonesnavn = new List<String>();
            foreach (Stasjon s in db.Stasjon)
            {
                alleStasjonesnavn.Add(s.Stasjonsnavn);
            }

            //sjekker om fra og til stasjoner finnes i databasen
            if (!alleStasjonesnavn.Contains(fraStasjon) || !alleStasjonesnavn.Contains(tilStasjon))
            {
                return false;
            }

            //sjekker om til og fra stasjon er lik
            if (fraStasjon == tilStasjon)
            {
                return false;
            }

            //sjekker at dato er på korrekt format
            DateTime date;
            bool validDate = DateTime.TryParseExact(
                dato, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
            if (!validDate)
            {
                return false;
            }

            //sjekker at avgang er på korrekt format
            DateTime time;
            bool validTime = DateTime.TryParseExact(
                avgang, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out time);
            if (!validTime)
            {
                return false;
            }

            //hvis det er bestilt retur, sjekker at returdato er på korrekt format
            if (returDato != null)
            {
                DateTime returDate;
                bool validReturDate = DateTime.TryParseExact(
                    returDato, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out returDate);
                if (!validReturDate)
                {
                    return false;
                }
            }

            //hvis det er bestilr retur, sjekker at returavgang er på korrekt format
            if (returAvgang != null)
            {
                DateTime returTime;
                bool validReturTime = DateTime.TryParseExact(
                    returAvgang, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out returTime);
                if (!validReturTime)
                {
                    return false;
                }
            }

            if (epost == null || epost == "")
            {
                return false;
            }
            else
            {
                //sjekker at epost er på korrekt format
                try
                {
                    MailAddress m = new MailAddress(epost);
                }
                catch (FormatException)
                {
                    return false;
                }
                return true;
            }
        }

        public bool lagreBestilling(bestilling innBestilling)
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

                catch (Exception)
                {
                    return false;
                }
            }
        }

        // ---------------------------------------------------------------------------------------
        // Metoder for å endre, slette og legge til stasjoner i DB 


    }
}