using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using HorseShow_Framework.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace HorseShow_Framework.DAO
{
    public class ShowDBContext : DbContext
    {
        public ShowDBContext() : base("ShowDBContext")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<ShowDBContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassPlacing> ClassPlacings { get; set; }
        public DbSet<ClassRegistration> ClassRegistrations { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<DivisionClassMapping> DivisionClassMappings { get; set; }
        public DbSet<GlobalShowSettings> GlobalShowSettings { get; set; }
        public DbSet<HorseRider> HorseRiders { get; set; }
        public DbSet<HorseRiderScores> HorseRiderScores { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<PlacePointValues> PointValues { get; set; }
        public DbSet<RiderAddOns> RiderAddOns { get; set; }
        public DbSet<StoreItems> StoreItems { get; set; }



    }
}