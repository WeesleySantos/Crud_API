using API.Data;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefaRepository _tarefaRepo;
        public TarefasController(ITarefaRepository tarefaRepo)
        {
            _tarefaRepo = tarefaRepo;
        }

        [HttpGet]
        [Route("tarefas")]
        public async Task<IActionResult> GetTarefas()
        {
            var resultado = await _tarefaRepo.GetTarefasAsync();
            return Ok(resultado);
        }

        [HttpGet]
        [Route("tarefa")]
        public async Task<IActionResult> GetTodoItemByIdAsync(int id)
        {
            var tarefa = await _tarefaRepo.GetTarefaByIdAsync(id);
            return Ok(tarefa);
        }

        [HttpGet]
        [Route("tarefascontador")]
        public async Task<IActionResult> GetTodosAndCountAsync()
        {
            var resultado = await _tarefaRepo.GetTarefasEContadorAsync();
            return Ok(resultado);
        }

        [HttpPost]
        [Route("criartarefa")]
        public async Task<IActionResult> SaveAsync(Tarefa novatarefa)
        {
            var resultado = await _tarefaRepo.SaveAsync(novatarefa);
            return Ok(resultado);
        }

        [HttpPost]
        [Route("atualizastatus")]
        public async Task<IActionResult> UpdateTodosStatusAsync(Tarefa atualizatarefa)
        {
            if (atualizatarefa.Id != 0)
            {
                var resultado = await _tarefaRepo.UpdateTarefaStatusAsync(atualizatarefa);
                return Ok(resultado);
            }
            return BadRequest("Tarefa não encontrada");
        }

        [HttpDelete]
        [Route("deletatarefa")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id != 0)
            {
                var resultado = await _tarefaRepo.DeleteAsync(id);
                return Ok(resultado);
            }
            return BadRequest("Tarefa não encontrada");
        }
    }
}
