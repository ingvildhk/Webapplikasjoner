using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Oppg1.Models
{
    public class Bane
    {
        [Key]
        public int BaneID { get; set; }
        public String Banenavn { get; set; } // Eks. linje L1, R10 etc. 
        public virtual List<StasjonPaaBane> StasjonPaaBane { get; set; }
    }

    public class StasjonPaaBane
    {
        [Key]
        public int StasjonPaaBaneID { get; set; }
        public virtual Stasjon Stasjon { get; set; }
        public virtual Bane Bane { get; set; }
        public String Avgang { get; set; }
    }

    public class Stasjon
    {
        [Key]
        public int StasjonsID { get; set; }
        public String Stasjonsnavn { get; set; }
    }

    public class Bestilling
    {
        [Key]
        public int BestillingsID { get; set; }
        public String fraStasjon { get; set; }
        public String tilStasjon { get; set; }
        public String dato { get; set; }
        public String returDato { get; set; }
        public String avgang { get; set; }
        public String returAvgang { get; set; }
        public String epost { get; set; }
    }
    public class DB : DbContext
    {
        public DB() : base("name=DB")
        {
            Database.CreateIfNotExists();

            Database.SetInitializer(new DBInit());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public virtual DbSet<Bane> Bane { get; set; }
        public virtual DbSet<StasjonPaaBane> StasjonPaaBane { get; set; }
        public virtual DbSet<Stasjon> Stasjon { get; set; }
        public virtual DbSet<Bestilling> Bestilling { get; set; }
    }
}