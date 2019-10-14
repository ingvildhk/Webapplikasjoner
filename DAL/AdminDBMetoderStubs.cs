using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    class AdminDBMetoderStubs : IAdminDBMetoder
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
            if (stasjon.Stasjonsnavn == "")
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
            if (id == 0)
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

        public bool leggTilStasjonPaaBane(stasjonPaaBane stasjonPaaBane, int stasjonID, int baneID)
        {
            if (stasjonPaaBane.Avgang == "")
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
            if (id == 0)
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
            if (bane.Banenavn == "")
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
            if (id == 0)
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
            if (stasjon.Stasjonsnavn == "")
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
            if (bane.Banenavn == "")
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
            if (stasjonPaaBane.Avgang == "")
            {
                return false;
            }

            else
            {
                return true;
            }
        }
    }
}
