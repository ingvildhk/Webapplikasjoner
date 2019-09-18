﻿using System;
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

            var nyAvgang1 = new Avgang
            {
                Avgangstider = new List<Avgangstider>()
            };
            nyAvgang1.Avgangstider.Add(nyAvgangstid1);
            nyAvgang1.Avgangstider.Add(nyAvgangstid2);

            var nyAvgang4 = new Avgang
            {
                Avgangstider = new List<Avgangstider>()
            };

            nyAvgang4.Avgangstider.Add(nyAvgangstid3);
            nyAvgang4.Avgangstider.Add(nyAvgangstid4);



            var nyStasjon1 = new Stasjon
            {
                Stasjonsnavn = "Oslo S",
                Avganger = new List<Avgang>()
            };

            nyStasjon1.Avganger.Add(nyAvgang1);

            var nyStasjon2 = new Stasjon
            {
                Stasjonsnavn = "Nationaltheateret",
                Avganger = new List<Avgang>()
            };
            nyStasjon2.Avganger.Add(nyAvgang4);

            var nyStasjon3 = new Stasjon
            {
                Stasjonsnavn = "Asker",
                Avganger = new List<Avgang>()
            };


            var nyStasjon4 = new Stasjon
            {
                Stasjonsnavn = "Skøyen",
                Avganger = new List<Avgang>()
            };

            


            var nyBane = new Bane
            {
                Banenavn = "Sørlandsbanen",
                Stasjoner = new List<Stasjon>()
            };

            nyBane.Stasjoner.Add(nyStasjon1);
            nyBane.Stasjoner.Add(nyStasjon2);
            nyBane.Stasjoner.Add(nyStasjon3);
            nyBane.Stasjoner.Add(nyStasjon4);

            nyAvgang1.Bane = nyBane;
            nyAvgang4.Bane = nyBane;

            var nyBestilling = new Bestilling
            {
                fraStasjon = nyStasjon1,
                tilStasjon = nyStasjon2,
                Avgangstid = nyAvgang4.Avgangstider[1],
                Dato = "10.10.2019",
                Navn = "Nina Olsen",
                Telefonnummer = "12345678"
            };

            context.Bestilling.Add(nyBestilling);
            base.Seed(context);
        }
    }
}