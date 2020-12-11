using System;

namespace BankAccountAPI.Models
{
    public class Transacao
    {

        public int Id { get; set; }
        public DateTime DataMovimentacao { get; set; }
        public string TipoMovimentacao { get; set; }
        public string Resultado { get; set; }
        public decimal ValorTentativa { get; set; }
        public decimal ValorPagoTotal { get; set; }
        public decimal Taxas { get; set; }
        public decimal ValorFinal { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal SaldoAtual { get; set; }
        public int ContaId { get; set; }
        public int? ContaRelacionadaId { get; set; }

        public void SetTransacao(string tipoMovimentacao, string resultado, decimal valorTentativa, decimal valorPagoTotal,
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
