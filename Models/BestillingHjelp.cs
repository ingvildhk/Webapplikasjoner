using System;
using System.ComponentModel.DataAnnotations;

namespace Oppg1.Models
{
    public class BestillingHjelp
    {
        public String fraStasjon { get; set; }
        public String tilStasjon { get; set; }
        public DateTime dato { get; set; }
        public DateTime returDato { get; set; }
        public String avgang { get; set; }
        public String returAvgang { get; set; }
        public String epost { get; set; }
    }
}