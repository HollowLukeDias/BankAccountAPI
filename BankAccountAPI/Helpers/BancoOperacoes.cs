using BankAccountAPI.Data;
using BankAccountAPI.Models;
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
            var informacoesDeDeposito = conta.Deposito(quantidade);

            var saldoAnterior   = informacoesDeDeposito.saldoAnterior;
            var saldoAtual      = informacoesDeDeposito.saldoAtual;
            var resultado       = informacoesDeDeposito.resultado;
            var taxa            = informacoesDeDeposito.valorTaxa;
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
            var informacoesDeSaque = conta.Saque(quantidade);

            var resultado       = informacoesDeSaque.resultado;
            var taxa            = informacoesDeSaque.valorTaxa;
            var saldoAnterior   = informacoesDeSaque.saldoAnterior;
            var saldoAtual      = informacoesDeSaque.saldoAtual;
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
            var informacoesDeTransferencia = conta.Transferencia(quantidade, contaDestino);

            var resultado = informacoesDeTransferencia.resultado;
            var taxa = informacoesDeTransferencia.valorTaxa;
            var saldoAnterior = informacoesDeTransferencia.saldoAnterior;
            var saldoAtual = informacoesDeTransferencia.saldoAtual;
            var saldoAnteriorDestino    = informacoesDeTransferencia.saldoAnteriorDestino;
            var saldoAtualDestino = informacoesDeTransferencia.saldoAtualDestino;
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
