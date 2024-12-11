using API.Models; 
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AlunoContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; } 
    }
}
