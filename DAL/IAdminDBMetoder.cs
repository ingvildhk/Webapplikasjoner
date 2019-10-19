using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IAdminDBMetoder
    {
        List<stasjon> hentAlleStasjoner();
        List<bane> hentAlleBaner();
        List<stasjonPaaBane> hentStasjonPaaBane(int id);
        stasjon hentEnStasjon(int id);
        bane hentEnBane(int id);
        stasjonPaaBane hentEnAvgang(int id);
        bool leggTilStasjon(stasjon stasjon);
        bool endreStasjon(int id, stasjon stasjon);
        bool slettStasjon(int id);
        bool leggTilStasjonPaaBane(string avgang, int stasjonID, int baneID);
        bool endreStasjonPaaBane(stasjonPaaBane stasjonPaaBane, int id);
        bool slettStasjonPaaBane(int stasjonPaaBaneID, int baneID);
        bool leggTilBane(bane bane);
        bool endreBane(int id, bane bane);
        bool slettBane(int id);
        bool sjekkStasjonOK(stasjon stasjon);
        bool sjekkBaneOK(bane bane);
        bool sjekkAvgangOK(stasjonPaaBane stasjonPaaBane);
        bool finnBrukerDB(bruker innbruker);
        String lagSalt();
        byte[] lagHash(String innstring);
    }
}
