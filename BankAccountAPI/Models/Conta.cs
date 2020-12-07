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
        private readonly float _taxaPorcentagemDeposito = 1/100F;
        private readonly float _taxaValorSaque = 4.00F;
        private readonly float _taxaValorTransferencia = 1.00F;

        [Key]
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

        /// <summary>
        /// Remove uma quantidade de dinheiro da conta
        /// </summary>
        /// <param name="quantidade"></param>
        /// <returns></returns>
        public (float saldoAnterior, float saldoAtual, string resultado, float valorTaxa) Saque(float quantidade)
        {
            var saldoAnterior = Saldo;
            if (Saldo >= quantidade)
            {
                Saldo -= quantidade;
                return (saldoAnterior, Saldo, resultado: "SUCESSO", valorTaxa: _taxaValorSaque);
            }
            return (Saldo, Saldo, resultado: "SALDO INSUFICIENTE", valorTaxa: _taxaValorSaque);
        }

        /// <summary>
        /// Deposita uma quantidade de dinheiro na conta
        /// </summary>
        /// <param name="quantidade"></param>
        /// <returns></returns>
        public (float saldoAnterior, float saldoAtual, string resultado, float valorTaxa) Deposito(float quantidade)
        {
            var saldoAnterior = Saldo;
            var taxa = quantidade * _taxaPorcentagemDeposito;
            var valorTaxado = quantidade - taxa;
            Saldo += valorTaxado;
            return (saldoAnterior, Saldo, resultado: "SUCESSO", valorTaxa: taxa);
        }

        /// <summary>
        /// Faz uma transferencia entre contas
        /// </summary>
        /// <param name="quantidade"></param>
        /// <param name="contaDestino"></param>
        /// <returns></returns>
        public (float saldoAnterior, float saldoAtual, string resultado, float valorTaxa, float saldoAnteriorDestino, float saldoAtualDestino)
            Transferencia(float quantidade, Conta contaDestino)
        {
            var saldoAnteriorDestino = contaDestino.Saldo;
            var saldoAnterior = Saldo;
            if (Saldo >= quantidade)
            {
                Saldo -= quantidade;
                contaDestino.Saldo += quantidade - _taxaValorTransferencia;
                return (saldoAnterior, Saldo, resultado: "SUCESSO", valorTaxa: _taxaValorTransferencia, saldoAnteriorDestino, contaDestino.Saldo);
            }
            return (Saldo, Saldo, resultado: "SALDO INSUFICIENTE", valorTaxa: _taxaValorTransferencia, saldoAnteriorDestino, saldoAnteriorDestino);
        }

    }
}
