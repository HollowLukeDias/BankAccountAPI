using BankAccountAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Services
{
    public class Saque
    {

        public Saque(Conta conta, float valor)
        {
            TentarSacar(conta, valor);
            GerarTransacao(conta.Id, valor);
        }



        public float SaldoAnterior  { get; set; }
        public float SaldoAtual     { get; set; }
        public string Resultado     { get; set; }
        public float ValorTaxa      { get; set; }
        public Transacao transacao  { get; set; }


        private void TentarSacar(Conta conta, float valor)
        {
            SaldoAnterior = conta.Saldo;

            if (valor <=  Conta.TaxaValorSaque || valor > conta.Saldo ){
                SaldoAtual = SaldoAnterior;
                Resultado = "FALHA";
                ValorTaxa = 0;
                return;
            }

            conta.AlterarSaldo(-valor);

            SaldoAtual = conta.Saldo;
            Resultado = "SUCESSO";
            ValorTaxa = Conta.TaxaValorSaque;
        }

        private void GerarTransacao(int contaId, float valorTentativa)
        {
            transacao = new Transacao();

            if (Resultado == "FALHA")
            {
                transacao.SetTransacao("SAQUE", Resultado, valorTentativa, 0, ValorTaxa, 0, SaldoAnterior, SaldoAtual, contaId, null);
                return;
            }

            var valorTaxado = valorTentativa - ValorTaxa;
            transacao.SetTransacao("SAQUE", Resultado, valorTentativa, valorTentativa, ValorTaxa, valorTaxado, SaldoAnterior, SaldoAtual, contaId, null);

        }
    }
}
