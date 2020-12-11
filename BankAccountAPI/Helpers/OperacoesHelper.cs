using BankAccountAPI.Data;
using BankAccountAPI.Services;

namespace BankAccountAPI.Helpers
{
    public class BancoOperacoes : IBanco
    {

        BancoDbContext bancoDbContext;

        public BancoOperacoes(BancoDbContext _bancoDbContext)
        {
            bancoDbContext = _bancoDbContext;
        }

        public void TransacaoDeposito(Conta conta, decimal quantidade)
        {
            var deposito = new Deposito(conta, quantidade);

            bancoDbContext.Transacoes.Add(deposito.transacao);
            bancoDbContext.SaveChanges(true);
        }

        public void TransacaoSaque(Conta conta, decimal quantidade)
        {
            var saque = new Saque(conta, quantidade);

            bancoDbContext.Transacoes.Add(saque.transacao);
            bancoDbContext.SaveChanges(true);
        }


        public void TransacaoTransferencia(Conta conta, Conta contaDestino, decimal quantidade)
        {
            var transferencia = new Transferencia(conta, contaDestino, quantidade);

            bancoDbContext.Transacoes.Add(transferencia.transacao);

            if (transferencia.Resultado == "SUCESSO")
                bancoDbContext.Transacoes.Add(transferencia.transacaoDestino);

            bancoDbContext.SaveChanges(true);
        }
    }
}
