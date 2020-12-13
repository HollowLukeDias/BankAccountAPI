using System.ComponentModel.DataAnnotations;

namespace BankAccountAPI
{
    public class Conta
    {
        protected internal static readonly decimal TaxaPorcentagemDeposito = 1 / 100M;
        protected internal static readonly decimal TaxaValorSaque = 4.00M;
        protected internal static readonly decimal TaxaValorTransferencia = 1.00M;

        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Nome do Cliente é obrigatório")]
        [RegularExpression(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Nome inválido")]
        public string NomeCliente { get; set; }

        public decimal Saldo { get; set; }

        public void AlterarSaldo(decimal valor)
        {
            Saldo += valor;
        }

    }
}
