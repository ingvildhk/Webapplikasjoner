using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public class VyBLL
    {
        public List<String> hentAlleStasjonsNavn()
        {
            var VyDal = new VyDBmetoder();
            List<String> alleStasjoner = VyDal.hentalleStasjonsNavn();
            return alleStasjoner;
        }

        public String hentStasjonsnavn(int id)
        {
            var VyDal = new VyDBmetoder();
            String stasjonsnavn = VyDal.hentStasjonsNavn(id);
            return stasjonsnavn;
        }

        public List<String> hentTilStasjonsNavn(String fraStasjonNavn)
        {
            var VyDal = new VyDBmetoder();
            List<String> tilStasjoner = VyDal.hentTilStasjonsNavn(fraStasjonNavn);
            return tilStasjoner;
        }

        public List<String> hentTidspunkt(String fraStasjon, String tilStasjon, String dato)
        {
            var VyDal = new VyDBmetoder();
            List<String> tidspunkt = VyDal.hentTidspunkt(fraStasjon, tilStasjon, dato);
            return tidspunkt;
        }

        public List<String> hentReturTidspunkt(String fraStasjon, String tilStasjon, string dato, string returDato, string avgang)
        {
            var VyDal = new VyDBmetoder();
            List<String> returTidspunkt = VyDal.hentReturTidspunkt(fraStasjon, tilStasjon, dato, returDato, avgang);
            return returTidspunkt;
        }

        public bool sjekkBestilling(bestilling innBestilling)
        {
            var VyDal = new VyDBmetoder();
            bool sjekk = VyDal.sjekkBestilling(innBestilling);
            return sjekk;
        }

        public bool lagreBestilling(bestilling innBestilling)
        {
            var VyDal = new VyDBmetoder();
            bool lagre = VyDal.lagreBestilling(innBestilling);
            return lagre;
        }
    }
}
