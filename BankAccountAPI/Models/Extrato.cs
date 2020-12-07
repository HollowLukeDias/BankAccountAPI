using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Models
{
    public class Extrato
    {

        public Extrato(string resultado, float valorPagoTotal, float taxas, float valorFinal, Conta contaOrigem, Conta contaDestino )
        { 
            _dataMovimentacao = DateTime.Now;
            _resultado = resultado;
            _valorPagoTotal = valorPagoTotal;
            _taxas = taxas;
            _valorFinal = valorFinal;
            _contaDestino = contaOrigem;
            _contaDestino = contaDestino;
        }

        public int Id                      { get; set; }
        public DateTime _dataMovimentacao   { get; set; }
        private string _resultado           { get; set; }
        private float _valorPagoTotal       { get; set; }
        private float _taxas                { get; set; }
        private float _valorFinal           { get; set; }
        private Conta _contaOrigem          { get; set; }
        private Conta _contaDestino         { get; set; }
        
    }
}
