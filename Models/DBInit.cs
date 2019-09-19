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
            var nyAvgangstid1 = new Avgangstider
            {
                Tidspunkt = "12.00"
            };

            var nyAvgangstid2 = new Avgangstider
            {
                Tidspunkt = "14.00"
            };

            var nyAvgangstid3 = new Avgangstider
            {
                Tidspunkt = "12.03"
            };

            var nyAvgangstid4 = new Avgangstider
            {
                Tidspunkt = "14.03"
            };

            var nyAvgang1 = new Avganger
            {
                Avgangstider = new List<Avgangstider>()
            };
            nyAvgang1.Avgangstider.Add(nyAvgangstid1);
            nyAvgang1.Avgangstider.Add(nyAvgangstid2);

            var nyAvgang2 = new Avganger
            {
                Avgangstider = new List<Avgangstider>()
            };

            nyAvgang2.Avgangstider.Add(nyAvgangstid3);
            nyAvgang2.Avgangstider.Add(nyAvgangstid4);

            var nyAvgang3 = new Avganger
            {
                Avgangstider = new List<Avgangstider>()
            };
            nyAvgang3.Avgangstider.Add(nyAvgangstid1);
            nyAvgang3.Avgangstider.Add(nyAvgangstid3);

            var nyAvgang4 = new Avganger
            {
                Avgangstider = new List<Avgangstider>()
            };
            nyAvgang4.Avgangstider.Add(nyAvgangstid2);
            nyAvgang4.Avgangstider.Add(nyAvgangstid4);

            var nyStasjon1 = new Stasjon
            {
                Stasjonsnavn = "Oslo S",
                Avganger = new List<Avganger>()
            };

            nyStasjon1.Avganger.Add(nyAvgang1);

            var nyStasjon2 = new Stasjon
            {
                Stasjonsnavn = "Nationaltheateret",
                Avganger = new List<Avganger>()
            };

            nyStasjon2.Avganger.Add(nyAvgang2);

            var nyStasjon3 = new Stasjon
            {
                Stasjonsnavn = "Oslo Lufthavn",
                Avganger = new List<Avganger>()
            };

            nyStasjon3.Avganger.Add(nyAvgang3);

            var nyStasjon4 = new Stasjon
            {
                Stasjonsnavn = "Lillestrøm",
                Avganger = new List<Avganger>()
            };

            nyStasjon4.Avganger.Add(nyAvgang4);

            var nyBane1 = new Bane
            {
                Banenavn = "R10",
                Stasjoner = new List<Stasjon>()
            };

            nyBane1.Stasjoner.Add(nyStasjon1);
            nyBane1.Stasjoner.Add(nyStasjon2);

            nyAvgang1.Bane = nyBane1;
            nyAvgang2.Bane = nyBane1;

            var nyBane2 = new Bane
            {
                Banenavn = "R11",
                Stasjoner = new List<Stasjon>()
            };

            nyBane2.Stasjoner.Add(nyStasjon3);
            nyBane2.Stasjoner.Add(nyStasjon4);

            nyAvgang3.Bane = nyBane2;
            nyAvgang4.Bane = nyBane2;

            context.Bane.Add(nyBane1);
            context.Bane.Add(nyBane2);
            base.Seed(context);
        }
    }
}