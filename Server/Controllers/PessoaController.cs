using Microsoft.AspNetCore.Mvc;
using PersonApi.Application;
using PersonApi.Domain;
using System;
using System.Threading.Tasks;

namespace PersonApi.Controllers
{
    [ApiController]
    [Route("api/pessoas")]
    public class PessoaController : ControllerBase
    {
        private readonly PessoaService _service;

        public PessoaController(PessoaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPessoas()
        {
            try
            {
                var pessoas = await _service.ListarPessoas();
                return Ok(pessoas);
            }
            catch (Exception ex)
            {
                // Tratar a exceção ou retornar uma resposta de erro apropriada
                return StatusCode(500, "Ocorreu um erro ao obter as pessoas.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPessoa(int id)
        {
            try
            {
                var pessoa = await _service.ObterPessoaPorId(id);
                if (pessoa == null)
                {
                    return NotFound();
                }
                return Ok(pessoa);
            }
            catch (Exception ex)
            {
                // Tratar a exceção ou retornar uma resposta de erro apropriada
                return StatusCode(500, "Ocorreu um erro ao obter a pessoa.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePessoa([FromBody] CreatePessoaModel pessoaModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var pessoa = new Pessoa
                {
                    Nome = pessoaModel.Nome,
                    Email = pessoaModel.Email,
                    Telefone = pessoaModel.Telefone
                };

                await _service.CriarPessoa(pessoa);

                return Ok();
            }
            catch (Exception ex)
            {
                // Tratar a exceção ou retornar uma resposta de erro apropriada
                return StatusCode(500, "Ocorreu um erro ao criar a pessoa.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePessoa(int id, [FromBody] Pessoa pessoa)
        {
            try
            {
                var pessoaExistente = await _service.ObterPessoaPorId(id);
                if (pessoaExistente == null)
                {
                    return NotFound();
                }
                pessoa.ID = id;
                await _service.AtualizarPessoa(pessoa);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Tratar a exceção ou retornar uma resposta de erro apropriada
                return StatusCode(500, "Ocorreu um erro ao atualizar a pessoa.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoa(int id)
        {
            try
            {
                var pessoaExistente = await _service.ObterPessoaPorId(id);
                if (pessoaExistente == null)
                {
                    return NotFound();
                }
                await _service.ExcluirPessoa(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Tratar a exceção ou retornar uma resposta de erro apropriada
                return StatusCode(500, "Ocorreu um erro ao excluir a pessoa.");
            }
        }
    }
}
