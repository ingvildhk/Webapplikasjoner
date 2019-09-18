using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oppg1.Models
{
    public class bane
    {
        public int baneID { get; set; }
        public string Banenavn { get; set; }
        public List<string> Stasjoner { get; set; }
    }
}