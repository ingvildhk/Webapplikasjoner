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


            // Her opprettes StasjonPåBane objekt

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
            NationalL1.Avgang.Add(kl12);


            // Oppretter Baner med stasjoner i liste
            var L1 = new Bane
            {
                Banenavn = "L1",
                StasjonPaaBane = new List<StasjonPaaBane>()
            };
            L1.StasjonPaaBane.Add(OslosL1);
            L1.StasjonPaaBane.Add(NationalL1);
        }
    }
}