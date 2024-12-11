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
    public class IMCsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IMCsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetIMC(int id)
        {
            var imc = _context.IMCs.Find(id);
            if (imc == null)
            {
                return NotFound();
            }

            var valorIMC = imc.ValorIMC;
            var dataCriacao = imc.DataCriacao;

            return Ok(new { valorIMC, dataCriacao });
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarIMC([FromBody] IMC imc)
        {
            var aluno = await _context.Alunos.FindAsync(imc.AlunoId);
            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado.");
            }

            imc.ValorIMC = imc.Peso / (imc.Altura * imc.Altura);
            imc.Classificacao = ClassificarIMC(imc.ValorIMC);
            imc.DataCriacao = DateTime.Now;

            _context.IMCs.Add(imc);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CadastrarIMC), new { id = imc.Id }, imc);
        }

        [HttpGet]
        public async Task<ActionResult> ListarIMCs()
        {
            var imcs = await _context.IMCs.Include(i => i.Aluno).ToListAsync();
            return Ok(imcs.Select(i => new {
                i.Id,
                i.Aluno.Nome,
                i.Aluno.Sobrenome,
                i.Altura,
                i.Peso,
                i.ValorIMC,
                i.Classificacao,
                i.DataCriacao
            }));
        }

        [HttpGet("aluno/{alunoId}")]
        public async Task<ActionResult> ListarIMCsPorAluno(int alunoId)
        {
            var imcs = await _context.IMCs
                .Where(i => i.AlunoId == alunoId)
                .Include(i => i.Aluno)
                .ToListAsync();

            return Ok(imcs.Select(i => new {
                i.Id,
                i.Altura,
                i.Peso,
                i.ValorIMC,
                i.Classificacao,
                i.DataCriacao
            }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarIMC(int id, [FromBody] IMC imc)
        {
            var existingImc = await _context.IMCs.FindAsync(id);
            if (existingImc == null)
            {
                return NotFound();
            }

            existingImc.Altura = imc.Altura;
            existingImc.Peso = imc.Peso;
            existingImc.ValorIMC = imc.Peso / (imc.Altura * imc.Altura);
            existingImc.Classificacao = ClassificarIMC(existingImc.ValorIMC);
            existingImc.DataCriacao = DateTime.Now;

            _context.IMCs.Update(existingImc);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private string ClassificarIMC(float imc)
        {
            if (imc < 18.5) return "Abaixo do peso";
            if (imc >= 18.5 && imc <= 24.9) return "Peso normal";
            if (imc >= 25 && imc <= 29.9) return "Sobrepeso";
            return "Obesidade";
        }
    }
}
