using Agenda.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Agenda.DataAccess
{
    public class AgendaContext : DbContext
    {
        public AgendaContext() : base("AgendaContext")
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonInfo> ContactInfo { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}