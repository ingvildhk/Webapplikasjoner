using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Oppg1.Models
{
    public class bestilling
    {
        public int BestillingId { get; set; }
        public string fraStasjon { get; set; }
        public string tilStasjon { get; set; }
        public string Avgangstid { get; set; }
        public string Dato { get; set; }
        public string Navn { get; set; }
        public string Telefonnummer { get; set; }
    }
}