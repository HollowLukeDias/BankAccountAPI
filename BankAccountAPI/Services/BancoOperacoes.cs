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

        ExtratoDbContext extratoDbContext;

        public BancoOperacoes(ExtratoDbContext _extratoDbContext)
        {
            extratoDbContext = _extratoDbContext;
        }

        public void Depositar(Conta conta, float quantidade)
        {
            var informacoesDeDeposito = conta.Deposito(quantidade);
            var aprovado = informacoesDeDeposito.aprovado;
            var resultado = informacoesDeDeposito.resultado;
            var taxa = informacoesDeDeposito.valorTaxa;
            var valorTaxado = quantidade - taxa;
            var extrato = new Extrato(resultado, quantidade, taxa, valorTaxado, conta, null);
            extratoDbContext.Extratos.Add(extrato);
            extratoDbContext.SaveChanges(true);
        }

        public void Saque(Conta conta, float quantidade)
        {
            throw new NotImplementedException();
        }

        public void Transferencia(Conta origem, Conta destino, float quantidade)
        {
            throw new NotImplementedException();
        }
    }
}
