using BankAccountAPI.Models;
using System;

namespace BankAccountAPI.Services
{
    public class Deposito
    {
        public Deposito(Conta conta, decimal valorTentativa)
        {
            TentarDepositar(conta, valorTentativa);
            GerarTransacao(conta.Id, valorTentativa);
        }

        public decimal SaldoAnterior { get; set; }
        public decimal SaldoAtual { get; set; }
        public string Resultado { get; set; }
        public decimal ValorTaxa { get; set; }
        public Transacao Transacao { get; set; }

        private void TentarDepositar(Conta conta, decimal valor)
        {
            SaldoAnterior = conta.Saldo;
            ValorTaxa = (decimal)Math.Round(valor * Conta.TaxaPorcentagemDeposito, 2);
            var valorTaxado = valor - ValorTaxa;

            conta.AlterarSaldo(valorTaxado);
            SaldoAtual = conta.Saldo;
            Resultado = "SUCESSO";
        }

        private void GerarTransacao(int contaId, decimal valorTentativa)
        {
            Transacao = new Transacao();
            var valorTotal = valorTentativa - ValorTaxa;
            Transacao.SetTransacao("DEPOSITO", Resultado, valorTentativa, valorTentativa, ValorTaxa, valorTotal, SaldoAnterior, SaldoAtual, contaId, null);
        }
    }
}
