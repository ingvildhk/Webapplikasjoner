using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class bruker
    {
        [Required (ErrorMessage = "Brukernavn må oppgis")]
        public String Brukernavn { get; set; }
        [Required(ErrorMessage = "Passord må oppgis")]
        public String Passord { get; set; }
    }
}
