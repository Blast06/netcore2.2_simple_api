using Microsoft.EntityFrameworkCore;

namespace cmdApi.Models
{
    public class CommandContext : DbContext
    {

        public CommandContext(DbContextOptions<CommandContext> options) : base (options)
        {
            
        }

        public DbSet<Command> CommandItems { get; set; }
        
    }
}