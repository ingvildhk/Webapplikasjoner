using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Oppg1.Models
{
    public class Avgang
    {
        [Key]
        public int avgangsID { get; set; }
        public virtual Bane Bane { get; set; }
        public virtual List<Avgangstider> Avgangstider { get; set; }
    }

    public class Avgangstider
    {
        [Key]
        public string Tidspunkt { get; set; }
    }

    public class Bane
    {
        [Key]
        public int baneID { get; set; }
        public string Banenavn { get; set; }
        public virtual List<Stasjon> Stasjoner { get; set; }
    }

    public class Bestilling
    {
        [Key]
        public int BestillingID { get; set; }
        public virtual Stasjon fraStasjon { get; set; }
        public virtual Stasjon tilStasjon { get; set; }
        public virtual Avgangstider Avgangstid { get; set; }
        [Display(Name="Dato")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0.dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Dato { get; set; }
    }

    public class Stasjon
    {
        [Key]
        public int stasjonsID { get; set; }
        public string Stasjonsnavn { get; set; }
        public virtual List<Avgang> Avganger { get; set; }
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

        public virtual DbSet<Avgang> Avgang { get; set; }
        public virtual DbSet<Bane> Bane { get; set; }
        public virtual DbSet<Bestilling> Bestilling { get; set; }
        public virtual DbSet<Stasjon> Stasjon { get; set; }
    }

}