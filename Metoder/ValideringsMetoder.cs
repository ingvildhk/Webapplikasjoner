using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Oppg1.Metoder
{
    public class ValideringsMetoder
    {

        //returnerer true om tidspunkt er på riktig format
        public bool sjekkTidspunkt(string tidspunkt)
        {
            DateTime tid;
            bool korrektTid = DateTime.TryParseExact(
                tidspunkt, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out tid);
            if (!korrektTid)
            {
                return false;
            }
            return true;
        }
    }
}