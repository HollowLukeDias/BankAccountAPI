using BankAccountAPI.Data;
using BankAccountAPI.Models;
using BankAccountAPI.Services;
using System.Collections.Generic;

namespace BankAccountAPI.Helpers
{
    public class BancoOperacoes : IBanco
    {

        BancoDbContext bancoDbContext;

        public BancoOperacoes(BancoDbContext _bancoDbContext)
        {
            bancoDbContext = _bancoDbContext;
        }

        public Transacao TransacaoDeposito(Conta conta, decimal quantidade)
        {
            var deposito = new Deposito(conta, quantidade);

            bancoDbContext.Transacoes.Add(deposito.Transacao);
            bancoDbContext.SaveChanges(true);

            return deposito.Transacao;
        }

        public Transacao TransacaoSaque(Conta conta, decimal quantidade)
        {
            var saque = new Saque(conta, quantidade);

            bancoDbContext.Transacoes.Add(saque.Transacao);
            bancoDbContext.SaveChanges(true);

            return saque.Transacao;
        }


        public Transacao TransacaoTransferencia(Conta conta, Conta contaDestino, decimal quantidade)
        {
            var transferencia = new Transferencia(conta, contaDestino, quantidade);

            bancoDbContext.Transacoes.Add(transferencia.Transacao);

            if (transferencia.Resultado == "SUCESSO")
                bancoDbContext.Transacoes.Add(transferencia.TransacaoDestino);

            bancoDbContext.SaveChanges(true);

            return transferencia.Transacao;
        }
    }
}
