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
        //----------------------------------------------------------------
        //Metoder for adminview
        public List<stasjon> hentAlleStasjoner()
        {
            var AdminDal = new AdminDBmetoder();
            List<stasjon> alleStasjoner = AdminDal.hentAlleStasjoner();
            return alleStasjoner;
        }

        public List<bane> hentAlleBaner()
        {
            var AdminDal = new AdminDBmetoder();
            List<bane> alleBaner = AdminDal.hentAlleBaner();
            return alleBaner;
        }

        public List<stasjonPaaBane> hentStasjonPaaBane(int stasjonID)
        {
            var AdminDal = new AdminDBmetoder();
            List<stasjonPaaBane> stasjonPaaBaner = AdminDal.hentStasjonPaaBane(stasjonID);
            return stasjonPaaBaner;
        }

        //----------------------------------------------------------------
        //Metoder for å lagre, endre, slette i database
        public bool leggTilStasjon(stasjon innStasjon)
        {
            var AdminDal = new AdminDBmetoder();
            return AdminDal.leggTilStasjon(innStasjon);
        }

        public bool endreStasjon(int stasjonID, stasjon innStasjon)
        {
            var AdminDal = new AdminDBmetoder();
            return AdminDal.endreStasjon(stasjonID, innStasjon);
        }

        public bool slettStasjon(int stasjonID)
        {
            var AdminDal = new AdminDBmetoder();
            return AdminDal.slettStasjon(stasjonID);
        }

        public bool leggTilStasjonPaaBane(stasjonPaaBane innStasjonPaaBane, int stasjonID, int baneID)
        {
            var AdminDal = new AdminDBmetoder();
            return AdminDal.leggTilStasjonPaaBane(innStasjonPaaBane, stasjonID, baneID);
        }

        public bool endreStasjonPaaBane(stasjonPaaBane innStasjonPaaBane, int stasjonID, int baneID, int stasjonPaaBaneID)
        {
            var AdminDal = new AdminDBmetoder();
            return AdminDal.endreStasjonPaaBane(innStasjonPaaBane, stasjonID, baneID, stasjonPaaBaneID);
        }

        public bool slettStasjonPaaBane(int stasjonPaaBaneID, int baneID)
        {
            var AdminDal = new AdminDBmetoder();
            return AdminDal.slettStasjonPaaBane(stasjonPaaBaneID, baneID);
        }

        public bool leggTilBane(bane innBane)
        {
            var AdminDal = new AdminDBmetoder();
            return AdminDal.leggTilBane(innBane);
        }

        public bool endreBane(int baneID, bane innBane)
        {
            var AdminDal = new AdminDBmetoder();
            return AdminDal.endreBane(baneID, innBane);
        }

        public bool slettBane(int baneID)
        {
            var AdminDal = new AdminDBmetoder();
            return AdminDal.slettBane(baneID);
        }

        //----------------------------------------------------------------
        //Metoder for bestillingsview
        public List<String> hentAlleStasjonsNavn()
        {
            var BestillingDal = new BestilligDBMetoder();
            List<String> alleStasjoner = BestillingDal.hentalleStasjonsNavn();
            return alleStasjoner;
        }

        public String hentStasjonsnavn(int id)
        {
            var BestillingDal = new BestilligDBMetoder();
            String stasjonsnavn = BestillingDal.hentStasjonsNavn(id);
            return stasjonsnavn;
        }

        public List<String> hentTilStasjonsNavn(String fraStasjonNavn)
        {
            var BestillingDal = new BestilligDBMetoder();
            List<String> tilStasjoner = BestillingDal.hentTilStasjonsNavn(fraStasjonNavn);
            return tilStasjoner;
        }

        public List<String> hentTidspunkt(String fraStasjon, String tilStasjon, String dato)
        {
            var BestillingDal = new BestilligDBMetoder();
            List<String> tidspunkt = BestillingDal.hentTidspunkt(fraStasjon, tilStasjon, dato);
            return tidspunkt;
        }

        public List<String> hentReturTidspunkt(String fraStasjon, String tilStasjon, string dato, string returDato, string avgang)
        {
            var BestillingDal = new BestilligDBMetoder();
            List<String> returTidspunkt = BestillingDal.hentReturTidspunkt(fraStasjon, tilStasjon, dato, returDato, avgang);
            return returTidspunkt;
        }

        public bool sjekkBestilling(bestilling innBestilling)
        {
            var BestillingDal = new BestilligDBMetoder();
            bool sjekk = BestillingDal.sjekkBestilling(innBestilling);
            return sjekk;
        }

        public bool lagreBestilling(bestilling innBestilling)
        {
            var BestillingDal = new BestilligDBMetoder();
            bool lagre = BestillingDal.lagreBestilling(innBestilling);
            return lagre;
        }
    }
}
