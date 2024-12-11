using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AlunosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarAluno([FromBody] Aluno aluno)
        {
            if (_context.Alunos.Any(a => a.Nome == aluno.Nome && a.Sobrenome == aluno.Sobrenome))
            {
                return BadRequest("Aluno já cadastrado.");
            }

            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CadastrarAluno), new { id = aluno.Id }, aluno);
        }
    }
}
