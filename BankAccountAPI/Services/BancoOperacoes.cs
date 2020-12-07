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

        public void Deposito(int id, float quantidade)
        {
            var conta = bancoDbContext.Contas.Find(id);
            var informacoesDeDeposito = conta.Deposito(quantidade);

            var saldoAnterior   = informacoesDeDeposito.saldoAnterior;
            var saldoAtual      = informacoesDeDeposito.saldoAtual;
            var resultado       = informacoesDeDeposito.resultado;
            var taxa            = informacoesDeDeposito.valorTaxa;
            var valorTaxado     = quantidade - taxa;

            var extrato = new Extrato();
            extrato.SetExtrato("DEPOSITO", resultado, quantidade, taxa, valorTaxado, saldoAnterior, saldoAtual, id, null);

            bancoDbContext.Extratos.Add(extrato);
            bancoDbContext.SaveChanges(true);
        }

        public void Saque(int id, float quantidade)
        {
            var conta = bancoDbContext.Contas.Find(id);
            var informacoesDeSaque = conta.Saque(quantidade);

            var resultado       = informacoesDeSaque.resultado;
            var taxa            = informacoesDeSaque.valorTaxa;
            var saldoAnterior   = informacoesDeSaque.saldoAnterior;
            var saldoAtual      = informacoesDeSaque.saldoAtual;
            var valorTaxado     = quantidade - taxa;

            var extrato = new Extrato();
            extrato.SetExtrato("SAQUE", resultado, quantidade, taxa, valorTaxado, saldoAnterior, saldoAtual, id, null);
            
            bancoDbContext.Extratos.Add(extrato);
            bancoDbContext.SaveChanges(true);
        }

        public void Transferencia(int id, int idDestino, float quantidade)
        {
            var conta = bancoDbContext.Contas.Find(id);
            var contaDestino = bancoDbContext.Contas.Find(idDestino);
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
                extratoContaOrigem.SetExtrato("TRANSFERENCIA - ENVIO", resultado, quantidade, taxa, valorTaxado, saldoAnterior, saldoAtual, id, idDestino);

                var extratoContaDestino = new Extrato();
                extratoContaDestino.SetExtrato("TRANFERENCIA - RECEBIMENTO", resultado, quantidade, taxa, valorTaxado, saldoAnteriorDestino, saldoAtualDestino, id, idDestino);

                bancoDbContext.Extratos.Add(extratoContaOrigem);
                bancoDbContext.Extratos.Add(extratoContaDestino);
            }
            else
            {
                var extratoContaOrigem = new Extrato();
                extratoContaOrigem.SetExtrato("TRANSFERENCIA - ENVIO", resultado, quantidade, taxa, valorTaxado, saldoAnterior, saldoAtual, id, idDestino);
                bancoDbContext.Extratos.Add(extratoContaOrigem);
            }

            bancoDbContext.SaveChanges(true);
        }
    }
}
