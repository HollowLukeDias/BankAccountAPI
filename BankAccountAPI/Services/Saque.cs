using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Services
{
    public class Saque
    {

        public Saque(Conta conta, float quantidade)
        {
            var info = TentarSacar(conta, quantidade);
            SaldoAnterior   = info.saldoAnterior;
            SaldoAtual      = info.saldoAtual;
            Resultado       = info.resultado;
            ValorTaxa       = info.valorTaxa;
        }

        public float SaldoAnterior  { get; set; }
        public float SaldoAtual     { get; set; }
        public string Resultado     { get; set; }
        public float ValorTaxa      { get; set; }


        private (float saldoAnterior, float saldoAtual, string resultado, float valorTaxa) TentarSacar(Conta conta, float quantidade)
        {
            var saldoAnterior = conta.Saldo;
            if (quantidade <=  Conta.TaxaValorSaque) return (conta.Saldo, conta.Saldo, resultado: "VALOR IGUAL OU MENOR QUE A TAXA", 0);
            if (conta.Saldo >= quantidade)
            {
                conta.Saldo -= quantidade;
                return (saldoAnterior, conta.Saldo, resultado: "SUCESSO", Conta.TaxaValorSaque);
            }
            return (conta.Saldo, conta.Saldo, resultado: "SALDO INSUFICIENTE", 0);
        }
    }
}
