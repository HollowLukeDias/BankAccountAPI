using BankAccountAPI.Models;

namespace BankAccountAPI.Services
{
    public class Transferencia
    {

        public Transferencia(Conta conta, Conta contaDestino, decimal valor)
        {
            TentarTransferencia(conta, contaDestino, valor);
            GerarTransacoes(conta.Id, contaDestino.Id, valor);
        }

        public decimal SaldoAnterior { get; set; }
        public decimal SaldoAtual { get; set; }
        public string Resultado { get; set; }
        public decimal ValorTaxa { get; set; }
        public decimal SaldoAnteriorDestino { get; set; }
        public decimal SaldoAtualDestino { get; set; }
        public Transacao transacao { get; set; }
        public Transacao transacaoDestino { get; set; }


        public void TentarTransferencia(Conta conta, Conta contaDestino, decimal valor)
        {
            SaldoAnteriorDestino = contaDestino.Saldo;
            SaldoAnterior = conta.Saldo;

            if (valor <= Conta.TaxaValorTransferencia || valor > conta.Saldo)
            {
                SaldoAtualDestino = SaldoAnteriorDestino;
                SaldoAtual = SaldoAnterior;
                Resultado = "FALHA";
                ValorTaxa = 0;
                return;
            }

            var valorTaxado = valor - Conta.TaxaValorTransferencia;

            conta.AlterarSaldo(-valor);
            contaDestino.AlterarSaldo(valorTaxado);

            SaldoAtual = conta.Saldo;
            Resultado = "SUCESSO";
            ValorTaxa = Conta.TaxaValorTransferencia;
            SaldoAtualDestino = contaDestino.Saldo;
        }

        private void GerarTransacoes(int contaId, int contaDestinoId, decimal valorTentativa)
        {
            transacao = new Transacao();

            if (Resultado == "FALHA")
            {
                transacao.SetTransacao("TRANSACAO - ENVIO", Resultado, valorTentativa, 0, 0, 0, SaldoAnterior, SaldoAtual, contaId, contaDestinoId);
                return;
            }

            var valorPago = SaldoAtual - SaldoAnterior;
            var valorTransferido = valorPago - Conta.TaxaValorTransferencia;
            transacao.SetTransacao("TRANSACAO - ENVIO", Resultado, valorTentativa, valorPago, Conta.TaxaValorTransferencia,
                                    valorTransferido, SaldoAnterior, SaldoAtual, contaId, contaDestinoId);

            transacaoDestino = new Transacao();
            transacaoDestino.SetTransacao("TRANSACAO - RECEBIMENTO", Resultado, valorTentativa, valorPago, Conta.TaxaValorTransferencia,
                                            valorTransferido, SaldoAnteriorDestino, SaldoAtualDestino, contaDestinoId, contaId);
        }

    }
}
