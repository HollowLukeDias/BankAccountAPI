using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Services
{
    public class Transferencia
    {

        public Transferencia(Conta conta, Conta contaDestino, float quantidade)
        {
            var info = TentarTransferencia(conta, contaDestino, quantidade);
            SaldoAnterior = info.saldoAnterior;
            SaldoAtual = info.saldoAtual;
            Resultado = info.resultado;
            ValorTaxa = info.valorTaxa;
            SaldoAnteriorDestino = info.saldoAnteriorDestino;
            SaldoAtualDestino = info.saldoAtualDestino;
        }

        public float SaldoAnterior          { get; set; }
        public float SaldoAtual             { get; set; }
        public string Resultado             { get; set; }
        public float ValorTaxa              { get; set; }
        public float SaldoAnteriorDestino   { get; set; }
        public float SaldoAtualDestino      { get; set; }

        /// <summary>
        /// Faz uma transferencia entre contas
        /// </summary>
        /// <param name="quantidade"></param>
        /// <param name="contaDestino"></param>
        /// <returns></returns>
        public (float saldoAnterior, float saldoAtual, string resultado, float valorTaxa, float saldoAnteriorDestino, float saldoAtualDestino)
            TentarTransferencia(Conta conta, Conta contaDestino, float quantidade)
        {
            var saldoAnteriorDestino = contaDestino.Saldo;
            var saldoAnterior = conta.Saldo;
            if (quantidade <= Conta.TaxaValorTransferencia) return (conta.Saldo, conta.Saldo, resultado: "VALOR IGUAL OU MENOR QUE A TAXA", 0, saldoAnteriorDestino, saldoAnteriorDestino);
            if (conta.Saldo >= quantidade)
            {
                conta.Saldo -= quantidade;
                contaDestino.Saldo += quantidade - Conta.TaxaValorTransferencia;
                return (saldoAnterior, conta. Saldo, resultado: "SUCESSO", Conta.TaxaValorTransferencia, saldoAnteriorDestino, contaDestino.Saldo);
            }
            return (conta.Saldo, conta.Saldo, resultado: "SALDO INSUFICIENTE", 0, saldoAnteriorDestino, saldoAnteriorDestino);
        }
    }
}
