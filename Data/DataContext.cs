using Microsoft.EntityFrameworkCore;
using Team12EUP.Entity;

namespace Team12EUP.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Account> accounts { get; set; }
        public DbSet<Advertisement> advertisements { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<HistoryTest> historyTests { get; set; }
        public DbSet<Question> questions { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Video> videos { get; set; }
        public DbSet<Voucher> vouchers { get; set; }
        public DbSet<Test> tests { get; set; }
        public DbSet<Supplier> suppliers { get; set; }
    }
}
