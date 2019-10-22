using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class AdminDBMetoderStubs : IAdminDBMetoder
    {
        public List<stasjon> hentAlleStasjoner()
        {
            var stasjonListe = new List<stasjon>();
            var stasjon = new stasjon()
            {
                StasjonID = 1,
                Stasjonsnavn = "Oslo S"
            };
            stasjonListe.Add(stasjon);
            stasjonListe.Add(stasjon);
            stasjonListe.Add(stasjon);
            stasjonListe.Add(stasjon);

            return stasjonListe;
        }

        public List<bane> hentAlleBaner()
        {
            var baneListe = new List<bane>();
            var bane = new bane()
            {
                BaneID = 1,
                Banenavn = "L1"
            };
            baneListe.Add(bane);
            baneListe.Add(bane);
            baneListe.Add(bane);
            baneListe.Add(bane);

            return baneListe;
        }

        public List<stasjonPaaBane> hentStasjonPaaBane(int id)
        {
            var stasjonPaaBaneListe = new List<stasjonPaaBane>();
            if (id == 0)
            {
                var stasjonPaaBane = new stasjonPaaBane();
                stasjonPaaBane.stasjonPaaBaneID = id;
                stasjonPaaBaneListe.Add(stasjonPaaBane);
                stasjonPaaBaneListe.Add(stasjonPaaBane);
                stasjonPaaBaneListe.Add(stasjonPaaBane);
                stasjonPaaBaneListe.Add(stasjonPaaBane);
                return stasjonPaaBaneListe;
            }

            else if (id == 2)
            {
                return stasjonPaaBaneListe;
            }

            else
            {
                var stasjonPaaBane = new stasjonPaaBane()
                {
                    stasjonPaaBaneID = 1,
                    StasjonsID = 1,
                    Stasjon = "Oslo S",
                    BaneID = 1,
                    Bane = "L1",
                    Avgang = "12:00"
                };
                stasjonPaaBaneListe.Add(stasjonPaaBane);
                stasjonPaaBaneListe.Add(stasjonPaaBane);
                stasjonPaaBaneListe.Add(stasjonPaaBane);
                stasjonPaaBaneListe.Add(stasjonPaaBane);

                return stasjonPaaBaneListe;
            }
        }

        public stasjon hentEnStasjon(int id)
        {
            if (id == 0)
            {
                var stasjon = new stasjon();
                stasjon.StasjonID = id;
                return stasjon;
            }

            else
            {
                var stasjon = new stasjon()
                {
                    StasjonID = 1,
                    Stasjonsnavn = "Oslo S"
                };
                return stasjon;
            }
        }

        public bane hentEnBane(int id)
        {
            if (id == 0)
            {
                var bane = new bane();
                bane.BaneID = id;
                return bane;
            }

            else
            {
                var bane = new bane()
                {
                    BaneID = 1,
                    Banenavn = "L1"
                };
                return bane;
            }
        }

        public stasjonPaaBane hentEnAvgang(int id)
        {
            if (id == 0)
            {
                var avgang = new stasjonPaaBane();
                avgang.stasjonPaaBaneID = id;
                return avgang;
            }

            else
            {
                var avgang = new stasjonPaaBane()
                {
                    stasjonPaaBaneID = 1,
                    BaneID = 1,
                    Bane = "L1",
                    StasjonsID = 1,
                    Stasjon = "Oslo S",
                    Avgang = "12:00"
                };
                return avgang;
            }
        }

        public bool leggTilStasjon(stasjon stasjon)
        {
            if (stasjon.Stasjonsnavn == "DetteErFeil")
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        public bool endreStasjon(int id, stasjon stasjon)
        {
            if (id == 10)
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        public bool slettStasjon(int id)
        {
            if (id == 0)
            {
                return false;
            }
            
            else
            {
                return true;
            }
        }

        public bool leggTilStasjonPaaBane(string avgang, int stasjonID, int baneID)
        {
            if (avgang == "10:10")
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        public bool endreStasjonPaaBane(stasjonPaaBane stasjonPaaBane, int id)
        {
            if (id == 10)
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        public bool slettStasjonPaaBane(int stasjonPaaBaneID, int baneID)
        {
            if (stasjonPaaBaneID == 0)
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        public bool leggTilBane(bane bane)
        {
            if (bane.Banenavn == "DetteErFeil")
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        public bool endreBane(int id, bane bane)
        {
            if (id == 10)
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        public bool slettBane(int id)
        {
            if (id == 0)
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        public bool sjekkStasjonOK(stasjon stasjon)
        {
            if (stasjon.Stasjonsnavn == "Feil")
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        public bool sjekkBaneOK(bane bane)
        {
            if (bane.Banenavn == "Feil")
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        public bool sjekkAvgangOK(stasjonPaaBane stasjonPaaBane)
        {
            if (stasjonPaaBane.Avgang == "10:01")
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        public bool finnBrukerDB(bruker innbruker)
        {
            if (innbruker.Brukernavn == "")
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public String lagSalt()
        {
            return "";
        }

        public byte[] lagHash(String innstring)
        {
            return Encoding.UTF8.GetBytes("");
        }
    }
}
