using System.ComponentModel.DataAnnotations;

namespace PersonApi.Controllers
{
    public class CreatePessoaModel
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Telefone { get; set; }
    }

}
