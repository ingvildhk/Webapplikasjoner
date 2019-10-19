using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Oppg1.Metoder
{
    public class ValideringsMetoder
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //returnerer true om tidspunkt er på riktig format
        public bool sjekkTidspunkt(string tidspunkt)
        {
            DateTime tid;
            bool korrektTid = DateTime.TryParseExact(
                tidspunkt, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out tid);
            if (!korrektTid)
            {
                log.Error("Feil i tid");
                return false;
            }
            return true;
        }
    }
}