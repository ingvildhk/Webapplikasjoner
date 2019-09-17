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
    }
}