﻿using BankAccountAPI.Data;
using BankAccountAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Helpers
{
    public class TransacaoRepository : ITransacoes
    {
        BancoDbContext bancoBdContext;

        public TransacaoRepository(BancoDbContext _bancoDbContext)
        {
            bancoBdContext = _bancoDbContext;
        }

        public IEnumerable<Transacao> GetTransacoes(int contaId, int page)
        {
            var pagination = (page - 1) * 30;
            var dataInicial = DateTime.Now.Date.AddDays(1 - pagination);
            var dataFinal = dataInicial.Date.AddDays(-30 - pagination);


            var queriedTransacoes = (from transacao in bancoBdContext.Transacoes
                                     where transacao.DataMovimentacao <= dataInicial
                                     && transacao.DataMovimentacao >= dataFinal
                                     && transacao.ContaId == contaId
                                     select transacao).ToList();

            return queriedTransacoes;
        }

        public Transacao GetTransacao(int id)
        {
            return bancoBdContext.Transacoes.Find(id);
        }
    }
}