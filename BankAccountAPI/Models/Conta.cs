using System.ComponentModel.DataAnnotations;

namespace BankAccountAPI
{
    public class Conta
    {
        internal const decimal TaxaPorcentagemDeposito = 1 / 100M;
        internal const decimal TaxaValorSaque = 4.00M;
        internal const decimal TaxaValorTransferencia = 1.00M;

        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Nome do Cliente é obrigatório")]
        [RegularExpression(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Nome inválido")]
        public string NomeCliente { get; set; }

        public decimal Saldo { get; private set; }

        public void AlterarSaldo(decimal valor)
        {
            Saldo += valor;
        }

    }
}
