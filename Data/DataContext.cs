using Microsoft.EntityFrameworkCore;

namespace Team12EUP.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    }
}
