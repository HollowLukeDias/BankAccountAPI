using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Services
{
    public class Deposito
    {
        public Deposito(Conta conta, float quantidade)
        {
            var info = TentarDepositar(conta, quantidade);
            SaldoAnterior   = info.saldoAnterior;
            SaldoAtual      = info.saldoAtual;
            Resultado       = info.resultado;
            ValorTaxa       = info.valorTaxa;

        }

        public float SaldoAnterior  { get; set; }
        public float SaldoAtual     { get; set; }
        public string Resultado     { get; set; }
        public float ValorTaxa      { get; set; }

        private (float saldoAnterior, float saldoAtual, string resultado, float valorTaxa) TentarDepositar(Conta conta, float quantidade)
        {
            var saldoAnterior = conta.Saldo;
            var taxa = quantidade * Conta.TaxaPorcentagemDeposito;
            var valorTaxado = quantidade - taxa;
            conta.Saldo += valorTaxado;
            return (saldoAnterior, conta.Saldo, "SUCESSO", taxa);
        }
    }
}
