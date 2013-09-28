using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Sample
{
    [Table("Client")]
    public class Client
    {
        [Required]
        [Key]
        public String ClientID { get; set; }

        public String ClientState { get; set; }
    }

    public class DBClient : DbContext
    {
        public DBClient()
        {
        }

        public DBClient(string connection) : base(connection)
        {
        }

        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DBClient>());
        }
    }
}
