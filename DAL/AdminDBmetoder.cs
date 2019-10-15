using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using Model;

namespace DAL
{
    //Nye metoder i Oppgave del 2)
    public class AdminDBmetoder : IAdminDBMetoder
    {
        //Metoder for å liste ut alle data i databasen i AdminView

        public List<stasjon> hentAlleStasjoner()
        {
            using (var db = new DB())
            {
                List<stasjon> alleStasjoner = db.Stasjon.Select(s => new stasjon()
                {
                    StasjonID = s.StasjonsID,
                    Stasjonsnavn = s.Stasjonsnavn
                }).ToList();

                return alleStasjoner;
            }
        }

        public List<bane> hentAlleBaner()
        {
            using (var db = new DB())
            {
                List<bane> alleBaner = db.Bane.Select(b => new bane()
                {
                    BaneID = b.BaneID,
                    Banenavn = b.Banenavn
                }).ToList();

                return alleBaner;
            }
        }

        public List<stasjonPaaBane> hentStasjonPaaBane(int stasjonID)
        {
            using (var db = new DB())
            {
                List<stasjonPaaBane> stasjonPaaBaner = new List<stasjonPaaBane>();

                foreach (StasjonPaaBane s in db.StasjonPaaBane)
                {
                    if (s.Stasjon.StasjonsID == stasjonID)
                    {
                        stasjonPaaBane stasjonPaaBane = new stasjonPaaBane()
                        {
                            stasjonPaaBaneID = s.StasjonPaaBaneID,
                            StasjonsID =  s.Stasjon.StasjonsID,
                            Stasjon = s.Stasjon.Stasjonsnavn,
                            BaneID = s.Bane.BaneID,
                            Bane = s.Bane.Banenavn,
                            Avgang = s.Avgang
                        };
                        stasjonPaaBaner.Add(stasjonPaaBane);
                    }
                }
                return stasjonPaaBaner;
            }
        }

        // Metode for å hente EN stasjon, EN bane

        public stasjon hentEnStasjon(int id)
        {
            using(var db = new DB())
            {
                var enDbStasjon = db.Stasjon.Find(id);
                if(enDbStasjon == null)
                {
                    return null;
                }
                else
                {
                    var utStasjon = new stasjon()
                    {
                        StasjonID = enDbStasjon.StasjonsID,
                        Stasjonsnavn = enDbStasjon.Stasjonsnavn
                    };
                    return utStasjon;
                }
            }
        }

        public bane hentEnBane (int id)
        {
            using (var db = new DB())
            {
                var enDbBane = db.Bane.Find(id);
                if (enDbBane == null)
                {
                    return null;
                }
                else
                {
                    var utBane = new bane()
                    {
                        BaneID = enDbBane.BaneID,
                        Banenavn = enDbBane.Banenavn
                    };
                    return utBane;
                }
            }
        }

        public stasjonPaaBane hentEnAvgang(int id)
        {
            using (var db = new DB())
            {
                var enDbAvgang = db.StasjonPaaBane.Find(id);
                if (enDbAvgang == null)
                {
                    return null;
                }
                else
                {
                    var utAvgang = new stasjonPaaBane()
                    {
                        stasjonPaaBaneID = enDbAvgang.StasjonPaaBaneID,
                        StasjonsID = enDbAvgang.Stasjon.StasjonsID,
                        Stasjon = enDbAvgang.Stasjon.Stasjonsnavn,
                        BaneID = enDbAvgang.Bane.BaneID,
                        Bane = enDbAvgang.Bane.Banenavn,
                        Avgang = enDbAvgang.Avgang

                    };
                    return utAvgang;
                }
            }
        }


        // ------------------------------------------------------------------------------------
        // Metoder for å endre, slette og legge til stasjoner i DB 
       
        public bool leggTilStasjon(stasjon innStasjon)
        {
            var nyStasjon = new Stasjon()
            {
                Stasjonsnavn = innStasjon.Stasjonsnavn
            };

            using (var db = new DB())
            {
                try
                {
                    db.Stasjon.Add(nyStasjon);
                    db.SaveChanges();
                    return true;
                }

                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool endreStasjon(int stasjonID, stasjon innStasjon)
        {
            using (var db = new DB())
            {
                try
                {
                    Stasjon endreStasjon = db.Stasjon.Find(stasjonID);
                    endreStasjon.Stasjonsnavn = innStasjon.Stasjonsnavn;
                    db.SaveChanges();
                    return true; 
                }
                
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public bool slettStasjon(int stasjonID)
        {
            using (var db = new DB())
            {
                try
                {
                    Stasjon stasjon = db.Stasjon.Find(stasjonID);
                    

                    //sletter alle StasjonPaaBane som inneholder stasjonen som slettes
                    List<StasjonPaaBane> slettStasjonPaaBaner = new List<StasjonPaaBane>();
                    foreach (StasjonPaaBane s in db.StasjonPaaBane)
                    {
                        if (s.Stasjon.StasjonsID == stasjonID)
                        {
                            slettStasjonPaaBaner.Add(s);
                        }
                    }
                    foreach (StasjonPaaBane s in slettStasjonPaaBaner)
                    {
                        db.StasjonPaaBane.Remove(s);
                    }
                    db.Stasjon.Remove(stasjon);


                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool leggTilStasjonPaaBane(string avgang, int stasjonID, int baneID)
        {
            var db = new DB();

            Stasjon innStasjon = db.Stasjon.Find(stasjonID);
            Bane innBane = db.Bane.Find(baneID);

            var nyStasjonPaaBane = new StasjonPaaBane()
            {
                Stasjon = innStasjon,
                Bane = innBane,
                Avgang = avgang
            };

            using (db)
            {
                try
                {
                    db.StasjonPaaBane.Add(nyStasjonPaaBane);

                    //legger til den nye StasjonPaaBane i Bane
                    innBane.StasjonPaaBane.Add(nyStasjonPaaBane);

                    db.SaveChanges();
                    return true;
                }

                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool endreStasjonPaaBane(stasjonPaaBane innStasjonPaaBane, int stasjonPaaBaneID)
        {
            using (var db = new DB())
            {
                try
                {
                    StasjonPaaBane EndreStasjonPaaBane = db.StasjonPaaBane.Find(stasjonPaaBaneID);
                    Stasjon innStasjon = db.Stasjon.Find(innStasjonPaaBane.StasjonsID);
                    Bane innBane = db.Bane.Find(innStasjonPaaBane.BaneID);

                    EndreStasjonPaaBane.Stasjon = innStasjon;
                    EndreStasjonPaaBane.Bane = innBane;
                    EndreStasjonPaaBane.Avgang = innStasjonPaaBane.Avgang;
                    db.SaveChanges();
                    return true;
                }

                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool slettStasjonPaaBane(int stasjonPaaBaneID, int baneID)
        {
            using (var db = new DB())
            {
                try
                {
                    StasjonPaaBane stasjonPaaBane = db.StasjonPaaBane.Find(stasjonPaaBaneID);
                    Bane bane = db.Bane.Find(baneID);
                    db.StasjonPaaBane.Remove(stasjonPaaBane);

                    //sletter StasjonPaaBane fra liste i Baner
                    bane.StasjonPaaBane.Remove(stasjonPaaBane);

                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

        }

        public bool leggTilBane(bane innBane)
        {
            var nyBane = new Bane()
            {
                Banenavn = innBane.Banenavn
            };
            nyBane.StasjonPaaBane = new List<StasjonPaaBane>();

            using (var db = new DB())
            {
                try
                {
                    db.Bane.Add(nyBane);
                    db.SaveChanges();
                    return true;
                }

                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool endreBane(int baneID, bane innBane)
        {
            using (var db = new DB())
            {
                try
                {
                    Bane endreBane = db.Bane.Find(baneID);
                    endreBane.Banenavn = innBane.Banenavn;
                    db.SaveChanges();
                    return true;
                }

                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool slettBane(int baneID)
        {
            using(var db = new DB())
            {
                try
                {
                    Bane bane = db.Bane.Find(baneID);
                    

                    //sletter alle StasjonPaaBaner som inneholder Banen som slettes
                    List<StasjonPaaBane> slettStasjonPaaBaner = new List<StasjonPaaBane>();
                    foreach (StasjonPaaBane s in db.StasjonPaaBane)
                    {
                        if (s.Bane.BaneID == baneID)
                        {
                            slettStasjonPaaBaner.Add(s);
                        }
                    }
                    foreach (StasjonPaaBane s in slettStasjonPaaBaner)
                    {
                        db.StasjonPaaBane.Remove(s);
                    }
                    db.Bane.Remove(bane);

                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        } 

        //------------------------------------------------------------------------------
        //Valideringsmetoder


        //returnerer false om stasjonen finnes fra før av
        public bool sjekkStasjonOK (stasjon innStasjon)
        {
            using (var db = new DB())
            {
                foreach (Stasjon s in db.Stasjon)
                {
                    if (s.Stasjonsnavn == innStasjon.Stasjonsnavn)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        //returnerer false om banen finnes fra før av
        public bool sjekkBaneOK (bane innBane)
        {
            using (var db = new DB())
            {
                foreach (Bane b in db.Bane)
                {
                    if (b.Banenavn == innBane.Banenavn)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        //returnerer false om avgangen finnes fra før av
        public bool sjekkAvgangOK (stasjonPaaBane innStasjonPaaBane)
        {
            using (var db = new DB())
            {
                List<StasjonPaaBane> avganger = new List<StasjonPaaBane>();

                foreach (StasjonPaaBane sb in db.StasjonPaaBane)
                {
                    if (sb.Bane.BaneID == innStasjonPaaBane.BaneID && sb.Stasjon.StasjonsID == innStasjonPaaBane.StasjonsID)
                    {
                        avganger.Add(sb);
                    }
                }

                foreach (StasjonPaaBane sb in avganger)
                {
                    if (sb.Avgang == innStasjonPaaBane.Avgang)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}