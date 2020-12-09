using BankAccountAPI.Data;
using BankAccountAPI.Models;
using BankAccountAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Helpers
{
    public class BancoOperacoes : IBanco
    {

        BancoDbContext bancoDbContext;

        public BancoOperacoes(BancoDbContext _bancoDbContext)
        {
            bancoDbContext = _bancoDbContext;
        }

        /// <summary>
        /// Deposita a quantidade que vem do endpoint, e usa as informações do depósito para preencher as informações da Transação
        /// </summary>
        /// <param name="conta"></param>
        /// <param name="quantidade"></param>
        public void TransacaoDeposito(Conta conta, float quantidade)
        {
            var deposito = new Deposito(conta, quantidade);

            var saldoAnterior   = deposito.SaldoAnterior;
            var saldoAtual      = deposito.SaldoAtual;
            var resultado       = deposito.Resultado;
            var taxa            = deposito.ValorTaxa;
            var valorTaxado     = quantidade - taxa;

            var transacao = new Transacao();
            transacao.SetTransacao("DEPOSITO", resultado, quantidade, taxa, valorTaxado, saldoAnterior, saldoAtual, conta.Id, null);

            bancoDbContext.Transacoes.Add(transacao);
            bancoDbContext.SaveChanges(true);
        }

        /// <summary>
        /// Faz o saque de uma quantidade que vem do endpoint, e usa as informações do saque para preencher as informações da transação
        /// </summary>
        /// <param name="conta"></param>
        /// <param name="quantidade"></param>
        public void TransacaoSaque(Conta conta, float quantidade)
        {
            var saque = new Saque(conta, quantidade);

            var resultado       = saque.Resultado;
            var taxa            = saque.ValorTaxa;
            var saldoAnterior   = saque.SaldoAnterior;
            var saldoAtual      = saque.SaldoAtual;
            var valorTaxado     = quantidade - taxa;

            var transacao = new Transacao();
            transacao.SetTransacao("SAQUE", resultado, quantidade, taxa, valorTaxado, saldoAnterior, saldoAtual, conta.Id, null);
            
            bancoDbContext.Transacoes.Add(transacao);
            bancoDbContext.SaveChanges(true);
        }

        /// <summary>
        /// Faz uma transferencia de um valor que vem do endpoint entre duas contas, se funcionar, preenche duas transações, uma pra cada conta
        /// Se não funcionar gera preenche a transação apenas para a conta que tentou fazer a transferencia
        /// </summary>
        /// <param name="conta"></param>
        /// <param name="contaDestino"></param>
        /// <param name="quantidade"></param>
        public void TransacaoTransferencia(Conta conta, Conta contaDestino, float quantidade)
        {
            var transferencia = new Transferencia(conta, contaDestino, quantidade);

            var resultado               = transferencia.Resultado;
            var taxa                    = transferencia.ValorTaxa;
            var saldoAnterior           = transferencia.SaldoAnterior;
            var saldoAtual              = transferencia.SaldoAtual;
            var saldoAnteriorDestino    = transferencia.SaldoAnteriorDestino;
            var saldoAtualDestino       = transferencia.SaldoAtualDestino;

            var valorTaxado = quantidade - taxa;

            if (resultado == "SUCESSO")
            {
                var transacaoContaOrigem = new Transacao();
                transacaoContaOrigem.SetTransacao("TRANSFERENCIA - ENVIO", resultado, quantidade, taxa, valorTaxado, saldoAnterior, saldoAtual, conta.Id, contaDestino.Id );

                var transacaoContaDestino = new Transacao();
                transacaoContaDestino.SetTransacao("TRANFERENCIA - RECEBIMENTO", resultado, quantidade, taxa, valorTaxado, saldoAnteriorDestino, saldoAtualDestino, contaDestino.Id, conta.Id);

                bancoDbContext.Transacoes.Add(transacaoContaOrigem);
                bancoDbContext.Transacoes.Add(transacaoContaDestino);
            }
            else
            {
                var transacaoContaOrigem = new Transacao();
                transacaoContaOrigem.SetTransacao("TRANSFERENCIA - ENVIO", resultado, quantidade, taxa, valorTaxado, saldoAnterior, saldoAtual, conta.Id, contaDestino.Id);
                bancoDbContext.Transacoes.Add(transacaoContaOrigem);
            }

            bancoDbContext.SaveChanges(true);
        }
    }
}
