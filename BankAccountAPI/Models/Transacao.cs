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
        public float ValorPagoTotal         { get; set; }
        public float Taxas                  { get; set; }
        public float ValorFinal             { get; set; }
        public float SaldoAnterior          { get; set; }
        public float SaldoAtual             { get; set; }
        public int ContaId                  { get; set; }
        public int? ContaRelacionadaId      { get; set; }


        /// <summary>
        /// Preenche os dados da Transação com todas as informações que ele precisa
        /// </summary>
        /// <param name="tipoMovimentacao"></param>
        /// <param name="resultado"></param>
        /// <param name="valorPagoTotal"></param>
        /// <param name="taxas"></param>
        /// <param name="valorFinal"></param>
        /// <param name="saldoAnterior"></param>
        /// <param name="saldoAtual"></param>
        /// <param name="contaId"></param>
        /// <param name="contaDestinoId"></param>
        public void SetTransacao(string tipoMovimentacao, string resultado, float valorPagoTotal,
                               float taxas, float valorFinal, float saldoAnterior,
                               float saldoAtual, int contaId, int? contaRelacionadaId)
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
            ContaRelacionadaId = contaRelacionadaId;
        }
    }
}
