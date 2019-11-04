using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMap.Models.Entities;

namespace WpfMap.Models.Contexts
{
    public class MainContext : DbContext
    {
        static MainContext()
        {
            Database.SetInitializer<MainContext>(new ContextInitializer());
        }

        public MainContext() : base("DbConnection") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomResident> RoomResidents { get; set; }
        public DbSet<Resident> Residents { get; set; }
    }
}
