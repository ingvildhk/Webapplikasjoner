using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Oppg1.Models
{
    public class DBInit : DropCreateDatabaseAlways<DB>
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

            // oppretter tidspunkt til listen i Togstopp

            TimeSpan kl09 = new TimeSpan(09, 00, 00);
            TimeSpan kl10 = new TimeSpan(10, 00, 00);
            TimeSpan kl11 = new TimeSpan(11, 00, 00);
            TimeSpan kl12 = new TimeSpan(12, 00, 00);
            TimeSpan kl13 = new TimeSpan(13, 00, 00);
            TimeSpan kl14 = new TimeSpan(14, 00, 00);
            TimeSpan kl15 = new TimeSpan(15, 00, 00);
            TimeSpan kl16 = new TimeSpan(16, 00, 00);
            TimeSpan kl17 = new TimeSpan(17, 00, 00);
            TimeSpan kl18 = new TimeSpan(18, 00, 00);
            TimeSpan kl19 = new TimeSpan(19, 00, 00);
            TimeSpan kl20 = new TimeSpan(20, 00, 00);


            //---------------- Her opprettes StasjonPaaBane objekt på bane L1--------------------

            var OslosL1 = new StasjonPaaBane
            {
                Stasjon = OsloS,
                Avgang = new List<TimeSpan>()
            };
            OslosL1.Avgang.Add(kl09);
            OslosL1.Avgang.Add(kl12);
            OslosL1.Avgang.Add(kl19);

            var NationalL1 = new StasjonPaaBane
            {
                Stasjon = Nationalteateret,
                Avgang = new List<TimeSpan>()
            };
            NationalL1.Avgang.Add(kl10);
            NationalL1.Avgang.Add(kl13);
            NationalL1.Avgang.Add(kl16);

            var SkoyenL1 = new StasjonPaaBane
            {
                Stasjon = Skoyen,
                Avgang = new List<TimeSpan>()
            };
            SkoyenL1.Avgang.Add(kl10);
            SkoyenL1.Avgang.Add(kl13);
            SkoyenL1.Avgang.Add(kl20);

            var AskerL1 = new StasjonPaaBane
            {
                Stasjon = Asker,
                Avgang = new List<TimeSpan>()
            };
            AskerL1.Avgang.Add(kl10);
            AskerL1.Avgang.Add(kl13);
            AskerL1.Avgang.Add(kl20);

            var LillestromL1 = new StasjonPaaBane
            {
                Stasjon = Lillestrom,
                Avgang = new List<TimeSpan>()
            };
            LillestromL1.Avgang.Add(kl10);
            LillestromL1.Avgang.Add(kl15);
            LillestromL1.Avgang.Add(kl17);



            // Oppretter Bane L1 med stasjoner i liste

            var L1 = new Bane
            {
                Banenavn = "L1",
                StasjonPaaBane = new List<StasjonPaaBane>()
            };
            L1.StasjonPaaBane.Add(OslosL1);
            L1.StasjonPaaBane.Add(NationalL1);
            L1.StasjonPaaBane.Add(SkoyenL1);
            L1.StasjonPaaBane.Add(AskerL1);
            L1.StasjonPaaBane.Add(LillestromL1);


            // --------------------------Oppretter Sørlandsbanen S20 --------------------

            var OslosS20 = new StasjonPaaBane
            {
                Stasjon = OsloS,
                Avgang = new List<TimeSpan>()
            };
            OslosS20.Avgang.Add(kl09);
            OslosS20.Avgang.Add(kl12);
            OslosS20.Avgang.Add(kl19);

            var StavS20 = new StasjonPaaBane
            {
                Stasjon = Stavanger,
                Avgang = new List<TimeSpan>()
            };
            StavS20.Avgang.Add(kl09);
            StavS20.Avgang.Add(kl12);
            StavS20.Avgang.Add(kl19);

            var AskerS20 = new StasjonPaaBane
            {
                Stasjon = Asker,
                Avgang = new List<TimeSpan>()
            };
            AskerS20.Avgang.Add(kl10);
            AskerS20.Avgang.Add(kl13);
            AskerS20.Avgang.Add(kl20);

            // S20 Bane med stasjoner

            var S20 = new Bane
            {
                Banenavn = "Sørlandsbanen",
                StasjonPaaBane = new List<StasjonPaaBane>()
            };
            S20.StasjonPaaBane.Add(OslosS20);
            S20.StasjonPaaBane.Add(StavS20);
            S20.StasjonPaaBane.Add(AskerS20);

            // -----------------------------Bane  R10 --------------------------------------
            var OslosR10 = new StasjonPaaBane
            {
                Stasjon = OsloS,
                Avgang = new List<TimeSpan>()
            };
            OslosR10.Avgang.Add(kl09);
            OslosR10.Avgang.Add(kl12);
            OslosR10.Avgang.Add(kl19);

            var JessR10 = new StasjonPaaBane
            {
                Stasjon = Jessheim,
                Avgang = new List<TimeSpan>()
            };
            JessR10.Avgang.Add(kl09);
            JessR10.Avgang.Add(kl12);
            JessR10.Avgang.Add(kl19);

            var GarR10 = new StasjonPaaBane
            {
                Stasjon = Gardermoen,
                Avgang = new List<TimeSpan>()
            };
            GarR10.Avgang.Add(kl10);
            GarR10.Avgang.Add(kl13);
            GarR10.Avgang.Add(kl20);

            // S20 Bane med stasjoner

            var R10 = new Bane
            {
                Banenavn = "R10",
                StasjonPaaBane = new List<StasjonPaaBane>()
            };
            S20.StasjonPaaBane.Add(OslosR10);
            S20.StasjonPaaBane.Add(JessR10);
            S20.StasjonPaaBane.Add(GarR10);


            // -----------------------------Bane  R20 --------------------------------------


            var OslosR20 = new StasjonPaaBane
            {
                Stasjon = OsloS,
                Avgang = new List<TimeSpan>()
            };
            OslosR20.Avgang.Add(kl09);
            OslosR20.Avgang.Add(kl12);
            OslosR20.Avgang.Add(kl19);

            var FredR20 = new StasjonPaaBane
            {
                Stasjon = Fredrikstad,
                Avgang = new List<TimeSpan>()
            };
            FredR20.Avgang.Add(kl09);
            FredR20.Avgang.Add(kl12);
            FredR20.Avgang.Add(kl18);

            var GarR20 = new StasjonPaaBane
            {
                Stasjon = Gardermoen,
                Avgang = new List<TimeSpan>()
            };
            GarR20.Avgang.Add(kl11);
            GarR20.Avgang.Add(kl14);
            GarR20.Avgang.Add(kl20);

            // S20 Bane med stasjoner

            var R20 = new Bane
            {
                Banenavn = "R20",
                StasjonPaaBane = new List<StasjonPaaBane>()
            };
            R20.StasjonPaaBane.Add(OslosR20);
            R20.StasjonPaaBane.Add(FredR20);
            R20.StasjonPaaBane.Add(GarR20);

            context.Bane.Add(L1);
            context.Bane.Add(S20);
            context.Bane.Add(R10);
            context.Bane.Add(R20);

            base.Seed(context);
        }
    }
}