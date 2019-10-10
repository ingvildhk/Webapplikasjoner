using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class stasjonPaaBane
    {
        public int stasjonPaaBaneID { get; set; }
        public string Stasjon { get; set; }
        public string Bane { get; set; }
        [Display(Name = "Tidspunkt")]
        [Required(ErrorMessage = "Tidspunkt må oppgis")]
        public string Avgang;
    }
}
