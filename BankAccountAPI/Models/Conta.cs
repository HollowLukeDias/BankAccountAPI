using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace BankAccountAPI
{
    public class Conta
    {
        [NotMapped]
        protected internal static readonly float TaxaPorcentagemDeposito = 1 / 100F;
        [NotMapped]
        protected internal static readonly float TaxaValorSaque = 4.00F;
        [NotMapped]
        protected internal static readonly float TaxaValorTransferencia = 1.00F;

        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Nome do Cliente é obrigatório")]
        [RegularExpression(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Nome inválido")]
        public string NomeCliente { get; set; }

        public float Saldo { get; set; }

        public void AlterarSaldo(float valor)
        {
            Saldo += valor;
        }

    }
}
