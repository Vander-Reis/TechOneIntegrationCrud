using PersonApi.Domain;
using PersonApi.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonApi.Application
{
    public class PessoaService
    {
        private readonly PessoaRepository _repository;

        public PessoaService(PessoaRepository repository)
        {
            _repository = repository;
        }

        public async Task CriarPessoa(Pessoa pessoa)
        {
            await _repository.CriarPessoa(pessoa);
        }

        public async Task<List<Pessoa>> ListarPessoas()
        {
            return await _repository.ListarPessoas();
        }

        public async Task<Pessoa> ObterPessoaPorId(int id)
        {
            return await _repository.ObterPessoaPorId(id);
        }

        public async Task AtualizarPessoa(Pessoa pessoa)
        {
            await _repository.AtualizarPessoa(pessoa);
        }

        public async Task ExcluirPessoa(int id)
        {
            await _repository.ExcluirPessoa(id);
        }
    }
}
