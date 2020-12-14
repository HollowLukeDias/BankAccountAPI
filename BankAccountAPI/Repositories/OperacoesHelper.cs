using BankAccountAPI.Data;
using BankAccountAPI.Models;
using BankAccountAPI.Services;
using System.Collections.Generic;

namespace BankAccountAPI.Helpers
{
    public class BancoOperacoes : IBanco
    {

        private readonly BancoDbContext _bancoDbContext;

        public BancoOperacoes(BancoDbContext _bancoDbContext)
        {
            this._bancoDbContext = _bancoDbContext;
        }

        public Transacao TransacaoDeposito(Conta conta, decimal quantidade)
        {
            var deposito = new Deposito(conta, quantidade);

            _bancoDbContext.Transacoes.Add(deposito.Transacao);
            _bancoDbContext.SaveChanges(true);

            return deposito.Transacao;
        }

        public Transacao TransacaoSaque(Conta conta, decimal quantidade)
        {
            var saque = new Saque(conta, quantidade);

            _bancoDbContext.Transacoes.Add(saque.Transacao);
            _bancoDbContext.SaveChanges(true);

            return saque.Transacao;
        }


        public Transacao TransacaoTransferencia(Conta conta, Conta contaDestino, decimal quantidade)
        {
            var transferencia = new Transferencia(conta, contaDestino, quantidade);

            _bancoDbContext.Transacoes.Add(transferencia.Transacao);

            if (transferencia.Resultado == "SUCESSO")
                _bancoDbContext.Transacoes.Add(transferencia.TransacaoDestino);

            _bancoDbContext.SaveChanges(true);

            return transferencia.Transacao;
        }
    }
}
