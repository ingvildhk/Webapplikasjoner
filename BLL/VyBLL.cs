using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public class VyBLL : IVyBLL
    {

        private IAdminDBMetoder _AdminDAL;

        public VyBLL()
        {
            _AdminDAL = new AdminDBmetoder();
        }

        public VyBLL(IAdminDBMetoder stub)
        {
            _AdminDAL = stub;
        }


        //----------------------------------------------------------------
        //Metoder for adminview
        public List<stasjon> hentAlleStasjoner()
        {
            List<stasjon> alleStasjoner = _AdminDAL.hentAlleStasjoner();
            return alleStasjoner;
        }

        public List<bane> hentAlleBaner()
        {
            List<bane> alleBaner = _AdminDAL.hentAlleBaner();
            return alleBaner;
        }

        public List<stasjonPaaBane> hentStasjonPaaBane(int stasjonID)
        {
            List<stasjonPaaBane> stasjonPaaBaner = _AdminDAL.hentStasjonPaaBane(stasjonID);
            return stasjonPaaBaner;
        }

        public stasjon hentEnStasjon(int id)
        {
            return _AdminDAL.hentEnStasjon(id);
        }

        public bane hentEnBane(int id)
        {
            return _AdminDAL.hentEnBane(id);
        }

        public stasjonPaaBane hentEnAvgang(int id)
        {
            return _AdminDAL.hentEnAvgang(id);
        }

        //----------------------------------------------------------------
        //Metoder for å lagre, endre, slette i database
        public bool leggTilStasjon(stasjon innStasjon)
        {
            return _AdminDAL.leggTilStasjon(innStasjon);
        }

        public bool endreStasjon(int stasjonID, stasjon innStasjon)
        {
            return _AdminDAL.endreStasjon(stasjonID, innStasjon);
        }

        public bool slettStasjon(int stasjonID)
        {
            return _AdminDAL.slettStasjon(stasjonID);
        }

        public bool leggTilStasjonPaaBane(string avgang, int stasjonID, int baneID)
        {
            return _AdminDAL.leggTilStasjonPaaBane(avgang, stasjonID, baneID);
        }

        public bool endreStasjonPaaBane(stasjonPaaBane innStasjonPaaBane, int stasjonPaaBaneID)
        {
            return _AdminDAL.endreStasjonPaaBane(innStasjonPaaBane, stasjonPaaBaneID);
        }

        public bool slettStasjonPaaBane(int stasjonPaaBaneID, int baneID)
        {
            return _AdminDAL.slettStasjonPaaBane(stasjonPaaBaneID, baneID);
        }

        public bool leggTilBane(bane innBane)
        {
            return _AdminDAL.leggTilBane(innBane);
        }

        public bool endreBane(int baneID, bane innBane)
        {
            return _AdminDAL.endreBane(baneID, innBane);
        }

        public bool slettBane(int baneID)
        {
            return _AdminDAL.slettBane(baneID);
        }

        //----------------------------------------------------------------
        //Valideringsmetoder
        public bool sjekkStasjonOK (stasjon innStasjon)
        {
            return _AdminDAL.sjekkStasjonOK(innStasjon);
        }

        public bool sjekkBaneOK (bane innBane)
        {
            return _AdminDAL.sjekkBaneOK(innBane);
        }

        public bool sjekkAvgangOK (stasjonPaaBane innStasjonPaaBane)
        {
            return _AdminDAL.sjekkAvgangOK(innStasjonPaaBane);
        }

        public bool finnBrukerDB(bruker innbruker)
        {
            return _AdminDAL.finnBrukerDB(innbruker);
        }

        //----------------------------------------------------------------
        //Metoder for bestillingsview
        public List<String> hentAlleStasjonsNavn()
        {
            var BestillingDal = new BestillingDBMetoder();
            List<String> alleStasjoner = BestillingDal.hentalleStasjonsNavn();
            return alleStasjoner;
        }

        public String hentStasjonsnavn(int id)
        {
            var BestillingDal = new BestillingDBMetoder();
            String stasjonsnavn = BestillingDal.hentStasjonsNavn(id);
            return stasjonsnavn;
        }

        public List<String> hentTilStasjonsNavn(String fraStasjonNavn)
        {
            var BestillingDal = new BestillingDBMetoder();
            List<String> tilStasjoner = BestillingDal.hentTilStasjonsNavn(fraStasjonNavn);
            return tilStasjoner;
        }

        public List<String> hentTidspunkt(String fraStasjon, String tilStasjon, String dato)
        {
            var BestillingDal = new BestillingDBMetoder();
            List<String> tidspunkt = BestillingDal.hentTidspunkt(fraStasjon, tilStasjon, dato);
            return tidspunkt;
        }

        public List<String> hentReturTidspunkt(String fraStasjon, String tilStasjon, string dato, string returDato, string avgang)
        {
            var BestillingDal = new BestillingDBMetoder();
            List<String> returTidspunkt = BestillingDal.hentReturTidspunkt(fraStasjon, tilStasjon, dato, returDato, avgang);
            return returTidspunkt;
        }

        public bool sjekkBestilling(bestilling innBestilling)
        {
            var BestillingDal = new BestillingDBMetoder();
            bool sjekk = BestillingDal.sjekkBestilling(innBestilling);
            return sjekk;
        }

        public bool lagreBestilling(bestilling innBestilling)
        {
            var BestillingDal = new BestillingDBMetoder();
            bool lagre = BestillingDal.lagreBestilling(innBestilling);
            return lagre;
        }

    }
}
