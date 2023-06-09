using PersonApi.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace PersonApi.Infrastructure
{
    public class PessoaRepository
    {
        private readonly ApplicationContext _context;

        public PessoaRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CriarPessoa(Pessoa pessoa)
        {
            try
            {
                var emailExists = await _context.Pessoas.FirstOrDefaultAsync(p => p.Email == pessoa.Email);
                if (emailExists != null)
                {
                    throw new InvalidOperationException("O e-mail já está cadastrado.");
                }

                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "INSERT INTO Pessoas (Nome, Email, Telefone) VALUES (@Nome, @Email, @Telefone)";
                    command.Parameters.Add(new SqliteParameter("@Nome", pessoa.Nome));
                    command.Parameters.Add(new SqliteParameter("@Email", pessoa.Email));
                    command.Parameters.Add(new SqliteParameter("@Telefone", pessoa.Telefone));

                    await _context.Database.OpenConnectionAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                // Tratar a exceção ou lançá-la para ser tratada em outro lugar
                throw;
            }
        }

        public async Task<List<Pessoa>> ListarPessoas()
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Pessoas";

                    await _context.Database.OpenConnectionAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var pessoas = new List<Pessoa>();
                        while (await reader.ReadAsync())
                        {
                            var pessoa = new Pessoa
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Telefone = reader.GetString(reader.GetOrdinal("Telefone"))
                            };
                            pessoas.Add(pessoa);
                        }
                        return pessoas;
                    }
                }
            }
            catch (Exception ex)
            {
                // Tratar a exceção ou lançá-la para ser tratada em outro lugar
                throw;
            }
        }

        public async Task<Pessoa> ObterPessoaPorId(int id)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Pessoas WHERE ID = @ID";
                    command.Parameters.Add(new SqliteParameter("@ID", id));

                    await _context.Database.OpenConnectionAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var pessoa = new Pessoa
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Telefone = reader.GetString(reader.GetOrdinal("Telefone"))
                            };
                            return pessoa;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                // Tratar a exceção ou lançá-la para ser tratada em outro lugar
                throw;
            }
        }

        public async Task AtualizarPessoa(Pessoa pessoa)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "UPDATE Pessoas SET Nome = @Nome, Email = @Email, Telefone = @Telefone WHERE ID = @ID";
                    command.Parameters.Add(new SqliteParameter("@Nome", pessoa.Nome));
                    command.Parameters.Add(new SqliteParameter("@Email", pessoa.Email));
                    command.Parameters.Add(new SqliteParameter("@Telefone", pessoa.Telefone));
                    command.Parameters.Add(new SqliteParameter("@ID", pessoa.ID));

                    await _context.Database.OpenConnectionAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                // Tratar a exceção ou lançá-la para ser tratada em outro lugar
                throw;
            }
        }

        public async Task ExcluirPessoa(int id)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "DELETE FROM Pessoas WHERE ID = @ID";
                    command.Parameters.Add(new SqliteParameter("@ID", id));

                    await _context.Database.OpenConnectionAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                // Tratar a exceção ou lançá-la para ser tratada em outro lugar
                throw;
            }
        }
    }
}
