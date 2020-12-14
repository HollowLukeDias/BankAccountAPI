using BankAccountAPI.Data;
using BankAccountAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountAPI.Helpers
{
    public class TransacaoRepository : ITransacoes
    {
        private readonly BancoDbContext _bancoBdContext;

        public TransacaoRepository(BancoDbContext _bancoDbContext)
        {
            _bancoBdContext = _bancoDbContext;
        }

        public IEnumerable<Transacao> GetTransacoes(int contaId, int page)
        {
            var pagination = (page - 1) * 30;
            var dataInicial = DateTime.Now.Date.AddDays(1 - pagination);
            var dataFinal = dataInicial.Date.AddDays(-30);


            var queriedTransacoes = (from transacao in _bancoBdContext.Transacoes
                                     where transacao.DataMovimentacao <= dataInicial
                                     && transacao.DataMovimentacao >= dataFinal
                                     && transacao.ContaId == contaId
                                     select transacao).ToList();

            return queriedTransacoes;
        }

        public Transacao GetTransacao(int idConta, int idTransacao)
        {
            var queriedTransacao = (from transacao in _bancoBdContext.Transacoes
                                    where transacao.Id == idTransacao
                                    && transacao.ContaId == idConta
                                    select transacao).First();

            return queriedTransacao;
        }
    }
}
