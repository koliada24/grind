using Microsoft.EntityFrameworkCore;

namespace ExampleMinimal
{
    public class TodoDB : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public TodoDB(DbContextOptions<TodoDB> options) : base(options)
        { 
        
        }
    }
}
