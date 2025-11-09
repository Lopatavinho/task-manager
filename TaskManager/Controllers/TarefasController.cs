using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TarefasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TarefasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tarefas = await _context.Tarefas.Include(t => t.Usuario).ToListAsync();
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) return NotFound();
            return Ok(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Tarefa tarefa)
        {
            var existente = await _context.Tarefas.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Titulo = tarefa.Titulo;
            existente.Descricao = tarefa.Descricao;
            existente.Status = tarefa.Status;
            await _context.SaveChangesAsync();

            return Ok(existente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) return NotFound();

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
