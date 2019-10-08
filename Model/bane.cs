using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class bane
    {
        public int BaneID { get; set; }

        [Display(Name = "Banenavn")]
        [Required(ErrorMessage = "Banenavn må oppgis")]
        public string Banenavn;
    }
}
