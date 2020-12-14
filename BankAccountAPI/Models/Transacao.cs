using System;

namespace BankAccountAPI.Models
{
    public class Transacao
    {

        public int Id { get; set; }
        public DateTime DataMovimentacao { get; private set; }
        public string TipoMovimentacao { get; private set; }
        public string Resultado { get; private set; }
        public decimal ValorTentativa { get; private set; }
        public decimal ValorPagoTotal { get; private set; }
        public decimal Taxas { get; private set; }
        public decimal ValorFinal { get; private set; }
        public decimal SaldoAnterior { get; private set; }
        public decimal SaldoAtual { get; private set; }
        public int ContaId { get; private set; }
        public int? ContaRelacionadaId { get; private set; }

        internal void SetTransacao(string tipoMovimentacao, string resultado, decimal valorTentativa, decimal valorPagoTotal,
                               decimal taxas, decimal valorFinal, decimal saldoAnterior,
                               decimal saldoAtual, int contaId, int? contaRelacionadaId)
        {
            DataMovimentacao = DateTime.UtcNow;
            TipoMovimentacao = tipoMovimentacao;
            Resultado = resultado;
            ValorPagoTotal = valorPagoTotal;
            Taxas = taxas;
            ValorTentativa = valorTentativa;
            ValorFinal = valorFinal;
            SaldoAnterior = saldoAnterior;
            SaldoAtual = saldoAtual;
            ContaId = contaId;
            ContaRelacionadaId = contaRelacionadaId;
        }
    }
}
