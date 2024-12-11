using Microsoft.EntityFrameworkCore;
using API.Models;  

namespace API.Data
{   
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<IMC> IMCs { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
    }
}
