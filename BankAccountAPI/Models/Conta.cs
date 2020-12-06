using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace BankAccountAPI
{
    public class Conta
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Nome do Cliente é obrigatório")]
        [RegularExpression(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Nome inválido")]
        public string NomeCliente { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        public string SenhaCliente { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){1,4})+)$", ErrorMessage = "Email não é válido")]
        public string Email { get; set; }

        public float Saldo { get; set; }
    }
}
