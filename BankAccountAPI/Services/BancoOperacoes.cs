using BankAccountAPI.Data;
using BankAccountAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Services
{
    public class BancoOperacoes : IBanco
    {

        BancoDbContext bancoDbContext;

        public BancoOperacoes(BancoDbContext _bancoDbContext)
        {
            bancoDbContext = _bancoDbContext;
        }

        /// <summary>
        /// Deposita a quantidade que vem do endpoint, e usa as informações do depósito para gerar o extrato
        /// </summary>
        /// <param name="conta"></param>
        /// <param name="quantidade"></param>
        public void DepositoGeraExtrato(Conta conta, float quantidade)
        {
            var informacoesDeDeposito = conta.Deposito(quantidade);

            var saldoAnterior   = informacoesDeDeposito.saldoAnterior;
            var saldoAtual      = informacoesDeDeposito.saldoAtual;
            var resultado       = informacoesDeDeposito.resultado;
            var taxa            = informacoesDeDeposito.valorTaxa;
            var valorTaxado     = quantidade - taxa;

            var extrato = new Extrato();
            extrato.SetExtrato("DEPOSITO", resultado, quantidade, taxa, valorTaxado, saldoAnterior, saldoAtual, conta.Id, null);

            bancoDbContext.Extratos.Add(extrato);
            bancoDbContext.SaveChanges(true);
        }

        /// <summary>
        /// Faz o saque de uma quantidade que vem do endpoint, e usa as informações do depósito para gerar o extrato 
        /// </summary>
        /// <param name="conta"></param>
        /// <param name="quantidade"></param>
        public void SaqueGeraExtrato(Conta conta, float quantidade)
        {
            var informacoesDeSaque = conta.Saque(quantidade);

            var resultado       = informacoesDeSaque.resultado;
            var taxa            = informacoesDeSaque.valorTaxa;
            var saldoAnterior   = informacoesDeSaque.saldoAnterior;
            var saldoAtual      = informacoesDeSaque.saldoAtual;
            var valorTaxado     = quantidade - taxa;

            var extrato = new Extrato();
            extrato.SetExtrato("SAQUE", resultado, quantidade, taxa, valorTaxado, saldoAnterior, saldoAtual, conta.Id, null);
            
            bancoDbContext.Extratos.Add(extrato);
            bancoDbContext.SaveChanges(true);
        }

        /// <summary>
        /// Faz uma transferencia de um valor que vem do endpoint entre duas contas, se funcionar, gera dois extratos, um pra cada conta
        /// Se não funcionar gera o extrato apenas para a conta que tentou fazer a transferencia
        /// </summary>
        /// <param name="conta"></param>
        /// <param name="contaDestino"></param>
        /// <param name="quantidade"></param>
        public void TransferenciaGeraExtrato(Conta conta, Conta contaDestino, float quantidade)
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
                var extratoContaOrigem = new Extrato();
                extratoContaOrigem.SetExtrato("TRANSFERENCIA - ENVIO", resultado, quantidade, taxa, valorTaxado, saldoAnterior, saldoAtual, conta.Id, contaDestino.Id );

                var extratoContaDestino = new Extrato();
                extratoContaDestino.SetExtrato("TRANFERENCIA - RECEBIMENTO", resultado, quantidade, taxa, valorTaxado, saldoAnteriorDestino, saldoAtualDestino, conta.Id, contaDestino.Id);

                bancoDbContext.Extratos.Add(extratoContaOrigem);
                bancoDbContext.Extratos.Add(extratoContaDestino);
            }
            else
            {
                var extratoContaOrigem = new Extrato();
                extratoContaOrigem.SetExtrato("TRANSFERENCIA - ENVIO", resultado, quantidade, taxa, valorTaxado, saldoAnterior, saldoAtual, conta.Id, contaDestino.Id);
                bancoDbContext.Extratos.Add(extratoContaOrigem);
            }

            bancoDbContext.SaveChanges(true);
        }
    }
}
