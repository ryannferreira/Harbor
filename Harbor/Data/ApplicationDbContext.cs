using Harbor.Models;
using Microsoft.EntityFrameworkCore;

namespace Harbor.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        
        }

        public DbSet<ContainerModel> Container { get; set; }
        public DbSet<MovimentacoesModel> Movimentacoes { get; set; }

    }
}
