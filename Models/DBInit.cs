using System.Collections.Generic;
using System.Data.Entity;

namespace Oppg1.Models
{
    public class DBInit : DropCreateDatabaseIfModelChanges<DB>
    {
        protected override void Seed(DB context)
        {
            // Oppretter stasjoner

            var OsloS = new Stasjon
            {
                Stasjonsnavn = "Oslo S"
            };
            var Nationalteateret = new Stasjon
            {
                Stasjonsnavn = "Nationalteateret"
            };
            var Skoyen = new Stasjon
            {
                Stasjonsnavn = "Skøyen"
            };
            var Asker = new Stasjon
            {
                Stasjonsnavn = "Asker"
            };
            var Lillestrom = new Stasjon
            {
                Stasjonsnavn = "Lillestrøm"
            };
            var Gardermoen = new Stasjon
            {
                Stasjonsnavn = "Gardermoen"
            };
            var Fredrikstad = new Stasjon
            {
                Stasjonsnavn = "Fredrikstad"
            };
            var Jessheim = new Stasjon
            {
                Stasjonsnavn = "Jessheim"
            };
            var Stavanger = new Stasjon
            {
                Stasjonsnavn = "Stavanger"
            };

            // Oppretter Banene

            var L1 = new Bane
            {
                Banenavn = "L1",
                StasjonPaaBane = new List<StasjonPaaBane>()
            };

            var R10 = new Bane
            {
                Banenavn = "R10",
                StasjonPaaBane = new List<StasjonPaaBane>()
            };

            var R20 = new Bane
            {
                Banenavn = "R20",
                StasjonPaaBane = new List<StasjonPaaBane>()
            };

            var S20 = new Bane
            {
                Banenavn = "Sørlandsbanen",
                StasjonPaaBane = new List<StasjonPaaBane>()
            };



            //---------------- Her opprettes StasjonPaaBane objekt på bane L1--------------------

            var OslosL1kl12 = new StasjonPaaBane
            {
                Stasjon = OsloS,
                Bane = L1,
                Avgang = "12:00"
            };

            var OslosL1kl14 = new StasjonPaaBane
            {
                Stasjon = OsloS,
                Bane = L1,
                Avgang = "14:00"
            };


            var NationalL1kl12 = new StasjonPaaBane
            {
                Stasjon = Nationalteateret,
                Bane = L1,
                Avgang = "12:05"
            };

            var NationalL1kl14 = new StasjonPaaBane
            {
                Stasjon = Nationalteateret,
                Bane = L1,
                Avgang = "14:05"
            };

            var SkoyenL1kl12 = new StasjonPaaBane
            {
                Stasjon = Skoyen,
                Bane = L1,
                Avgang = "12:15"
            };

            var SkoyenL1kl14 = new StasjonPaaBane
            {
                Stasjon = Skoyen,
                Bane = L1,
                Avgang = "14:15"
            };

            var AskerL1kl12 = new StasjonPaaBane
            {
                Stasjon = Skoyen,
                Bane = L1,
                Avgang = "12:25"
            };

            var AskerL1kl14 = new StasjonPaaBane
            {
                Stasjon = Skoyen,
                Bane = L1,
                Avgang = "14:25"
            };

            //Legger til stasjonene i L1
            L1.StasjonPaaBane.Add(OslosL1kl12);
            L1.StasjonPaaBane.Add(OslosL1kl14);
            L1.StasjonPaaBane.Add(NationalL1kl12);
            L1.StasjonPaaBane.Add(NationalL1kl14);
            L1.StasjonPaaBane.Add(SkoyenL1kl12);
            L1.StasjonPaaBane.Add(SkoyenL1kl14);
            L1.StasjonPaaBane.Add(AskerL1kl12);
            L1.StasjonPaaBane.Add(AskerL1kl14);


            // --------------------------Oppretter Sørlandsbanen S20 --------------------

            var OslosS20kl1430 = new StasjonPaaBane
            {
                Stasjon = OsloS,
                Bane = S20,
                Avgang = "14:30"
            };

            var OslosS20kl19 = new StasjonPaaBane
            {
                Stasjon = OsloS,
                Bane = S20,
                Avgang = "19:00"
            };


            var StavS20kl08 = new StasjonPaaBane
            {
                Stasjon = Stavanger,
                Bane = S20,
                Avgang = "08:00"
            };

            var StavS20kl16 = new StasjonPaaBane
            {
                Stasjon = Stavanger,
                Bane = S20,
                Avgang = "16:00"
            };

            var AskerS20kl0830 = new StasjonPaaBane
            {
                Stasjon = Asker,
                Bane = S20,
                Avgang = "08:30"
            };

            var AskerS20kl1930 = new StasjonPaaBane
            {
                Stasjon = Asker,
                Bane = S20,
                Avgang = "19:30"
            };

            S20.StasjonPaaBane.Add(OslosS20kl1430);
            S20.StasjonPaaBane.Add(OslosS20kl19);
            S20.StasjonPaaBane.Add(StavS20kl08);
            S20.StasjonPaaBane.Add(StavS20kl16);
            S20.StasjonPaaBane.Add(AskerS20kl0830);
            S20.StasjonPaaBane.Add(AskerS20kl1930);

            // -----------------------------Bane  R10 --------------------------------------
            var OslosR10kl10 = new StasjonPaaBane
            {
                Stasjon = OsloS,
                Bane = R10,
                Avgang = "10:00"
            };

            var OslosR10kl13 = new StasjonPaaBane
            {
                Stasjon = OsloS,
                Bane = R10,
                Avgang = "13:00"
            };

            var JessR10kl11 = new StasjonPaaBane
            {
                Stasjon = Jessheim,
                Bane = R10,
                Avgang = "11:00"
            };

            var JessR10kl14 = new StasjonPaaBane
            {
                Stasjon = Jessheim,
                Bane = R10,
                Avgang = "14:00"
            };

            var GarR10kl1130 = new StasjonPaaBane
            {
                Stasjon = Gardermoen,
                Bane = R10,
                Avgang = "11:30"
            };

            var GarR10kl1430 = new StasjonPaaBane
            {
                Stasjon = Gardermoen,
                Bane = R10,
                Avgang = "14:30"
            };

            // R10 Bane med stasjoner

            R10.StasjonPaaBane.Add(OslosR10kl10);
            R10.StasjonPaaBane.Add(OslosR10kl13);
            R10.StasjonPaaBane.Add(JessR10kl11);
            R10.StasjonPaaBane.Add(JessR10kl14);
            R10.StasjonPaaBane.Add(GarR10kl1130);
            R10.StasjonPaaBane.Add(GarR10kl1430);

            // -----------------------------Bane  R20 --------------------------------------


            var OslosR20kl17 = new StasjonPaaBane
            {
                Stasjon = OsloS,
                Bane = R20,
                Avgang = "17.00"
            };

            var OslosR20kl19 = new StasjonPaaBane
            {
                Stasjon = OsloS,
                Bane = R20,
                Avgang = "19.00"
            };

            var FredR20kl18 = new StasjonPaaBane
            {
                Stasjon = Fredrikstad,
                Bane = R20,
                Avgang = "18.00"
            };

            var FredR20kl20 = new StasjonPaaBane
            {
                Stasjon = Fredrikstad,
                Bane = R20,
                Avgang = "20.00"
            };

            var GarR20kl16 = new StasjonPaaBane
            {
                Stasjon = Gardermoen,
                Bane = R20,
                Avgang = "16.00"
            };

            var GarR20kl18 = new StasjonPaaBane
            {
                Stasjon = Gardermoen,
                Bane = R20,
                Avgang = "18.00"
            };

            R20.StasjonPaaBane.Add(OslosR20kl17);
            R20.StasjonPaaBane.Add(OslosR20kl19);
            R20.StasjonPaaBane.Add(FredR20kl18);
            R20.StasjonPaaBane.Add(FredR20kl20);
            R20.StasjonPaaBane.Add(GarR20kl16);
            R20.StasjonPaaBane.Add(GarR20kl18);

            context.Bane.Add(L1);
            context.Bane.Add(S20);
            context.Bane.Add(R10);
            context.Bane.Add(R20);
            base.Seed(context);
        }
    }
}