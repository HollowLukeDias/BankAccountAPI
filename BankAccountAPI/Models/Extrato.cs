using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Models
{
    public class Extrato
    {

        public int Id                       { get; set; }
        public DateTime DataMovimentacao    { get; set; }
        public string TipoMovimentacao      { get; set; }
        public string Resultado             { get; set; }
        public float ValorPagoTotal         { get; set; }
        public float Taxas                  { get; set; }
        public float ValorFinal             { get; set; }
        public float SaldoAnterior          { get; set; }
        public float SaldoAtual             { get; set; }
        public int ContaId                  { get; set; }
        public int? ContaDestinoId          { get; set; }

        public void SetExtrato(string tipoMovimentacao, string resultado, float valorPagoTotal,
                               float taxas, float valorFinal, float saldoAnterior,
                               float saldoAtual, int contaId, int? contaDestinoId)
        {
            DataMovimentacao = DateTime.Now;
            TipoMovimentacao = tipoMovimentacao;
            Resultado = resultado;
            ValorPagoTotal = valorPagoTotal;
            Taxas = taxas;
            ValorFinal = valorFinal;
            SaldoAnterior = saldoAnterior;
            SaldoAtual = saldoAtual;
            ContaId = contaId;
            ContaDestinoId = contaDestinoId;
        }
    }
}
