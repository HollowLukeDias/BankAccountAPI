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
        private readonly float _taxaPorcentagemDeposito = 1/100;
        private readonly float _taxaValorSaque = 4.00F;
        private readonly float _taxaValorTransferencia = 1.00F;


        public int Id { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Nome do Cliente é obrigatório")]
        [RegularExpression(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Nome inválido")]
        public string NomeCliente { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        public string SenhaCliente { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){1,4})+)$", ErrorMessage = "Email não é válido")]
        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }

        public float Saldo { get; set; }
    
    
        public (bool aprovado, string resultado, float valorTaxa) Saque(float quantidade)
        {
            if (Saldo >= quantidade)
            {
                Saldo -= quantidade;
                return (aprovado: true, resultado: "SUCESSO", valorTaxa: _taxaValorSaque);
            }
            return (aprovado: false, resultado: "SALDO INSUFICIENTE", valorTaxa: _taxaValorSaque);
        }

        public (bool aprovado, string resultado, float valorTaxa) Deposito(float quantidade)
        {
            var taxa = quantidade * _taxaPorcentagemDeposito;
            var valorTaxado = quantidade - taxa;
            Saldo += quantidade;
            return (aprovado: true, resultado: "SUCESSO", valorTaxa: taxa)
        }

    }
}
