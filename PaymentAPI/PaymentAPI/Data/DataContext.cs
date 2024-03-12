using Microsoft.EntityFrameworkCore;
using PaymentAPI.Model;

namespace PaymentAPI.Data
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<TransactionDetails> TransactionDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().ToTable("users");
            modelBuilder.Entity<Accounts>().ToTable("accounts");
            modelBuilder.Entity<Transactions>().ToTable("transactions");
            modelBuilder.Entity<TransactionDetails>().HasNoKey().ToView("TransactionDetails");

        }
    }
}
