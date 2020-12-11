using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Models
{
    public class Transacao
    {

        public int Id                       { get; set; }
        public DateTime DataMovimentacao    { get; set; }
        public string TipoMovimentacao      { get; set; }
        public string Resultado             { get; set; }
        public float ValorTentativa         { get; set; }
        public float ValorPagoTotal         { get; set; }
        public float Taxas                  { get; set; }
        public float ValorFinal             { get; set; }
        public float SaldoAnterior          { get; set; }
        public float SaldoAtual             { get; set; }
        public int ContaId                  { get; set; }
        public int? ContaRelacionadaId      { get; set; }

        public void SetTransacao(string tipoMovimentacao, string resultado, float valorTentativa, float valorPagoTotal,
                               float taxas, float valorFinal, float saldoAnterior,
                               float saldoAtual, int contaId, int? contaRelacionadaId)
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
