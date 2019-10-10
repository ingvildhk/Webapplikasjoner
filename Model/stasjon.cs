using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class stasjon
    {
        public int StasjonID { get; set; }

        [Display(Name = "Stasjonsnavn")]
        [Required(ErrorMessage = "Stasjonsnavn må oppgis")]
        public string Stasjonsnavn;
    }
}
