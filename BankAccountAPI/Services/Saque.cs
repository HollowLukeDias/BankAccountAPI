using BankAccountAPI.Models;

namespace BankAccountAPI.Services
{
    public class Saque
    {

        public Saque(Conta conta, decimal valor)
        {
            TentarSacar(conta, valor);
            GerarTransacao(conta.Id, valor);
        }



        public decimal SaldoAnterior { get; set; }
        public decimal SaldoAtual { get; set; }
        public string Resultado { get; set; }
        public decimal ValorTaxa { get; set; }
        public Transacao Transacao { get; set; }


        private void TentarSacar(Conta conta, decimal valor)
        {
            SaldoAnterior = conta.Saldo;

            if (valor <= Conta.TaxaValorSaque || valor > conta.Saldo)
            {
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

        private void GerarTransacao(int contaId, decimal valorTentativa)
        {
            Transacao = new Transacao();

            if (Resultado == "FALHA")
            {
                Transacao.SetTransacao("SAQUE", Resultado, valorTentativa, 0, ValorTaxa, 0, SaldoAnterior, SaldoAtual, contaId, null);
                return;
            }

            var valorTaxado = valorTentativa - ValorTaxa;
            Transacao.SetTransacao("SAQUE", Resultado, valorTentativa, valorTentativa, ValorTaxa, valorTaxado, SaldoAnterior, SaldoAtual, contaId, null);

        }
    }
}
