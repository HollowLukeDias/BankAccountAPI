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

        public void Depositar(int id, float quantidade)
        {
            var conta = bancoDbContext.Contas.Find(id);
            var informacoesDeDeposito = conta.Deposito(quantidade);

            var saldoAnterior = informacoesDeDeposito.saldoAnterior;
            var saldoAtual = informacoesDeDeposito.saldoAtual;
            var resultado = informacoesDeDeposito.resultado;
            var taxa = informacoesDeDeposito.valorTaxa;
            var valorTaxado = quantidade - taxa;

            var extrato = new Extrato();
            extrato.SetExtrato("DEPOSITO", resultado, quantidade, taxa, valorTaxado, saldoAnterior, saldoAtual, id, null);
            bancoDbContext.Extratos.Add(extrato);
            bancoDbContext.SaveChanges(true);
        }

        public void Saque(int id, float quantidade)
        {
            var conta = bancoDbContext.Contas.Find(id);
            Console.WriteLine($"what? {quantidade}");
            var informacoesDeDeposito = conta.Saque(quantidade);

            var saldoAnterior = informacoesDeDeposito.saldoAnterior;
            var saldoAtual = informacoesDeDeposito.saldoAtual;
            var resultado = informacoesDeDeposito.resultado;
            var taxa = informacoesDeDeposito.valorTaxa;
            var valorTaxado = quantidade - taxa;

            var extrato = new Extrato();
            extrato.SetExtrato("SAQUE", resultado, quantidade, taxa, valorTaxado, saldoAnterior, saldoAtual, id, null);
            bancoDbContext.Extratos.Add(extrato);
            bancoDbContext.SaveChanges(true);
        }

        public void Transferencia(Conta origem, Conta destino, float quantidade)
        {
            throw new NotImplementedException();
        }
    }
}
