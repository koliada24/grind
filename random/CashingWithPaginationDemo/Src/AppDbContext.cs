using System;
using Microsoft.EntityFrameworkCore;

namespace CashingWithPaginationDemo.Src
{
    public class AppDbContext : DbContext
    {
        public DbSet<EntityModel> EntityModels { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions)
            : base(dbContextOptions) { }
    }
}
