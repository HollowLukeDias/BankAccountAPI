using BankAccountAPI.Data;
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

        public IEnumerable<Transacao> GetTransacoes(int contaId, DateTime dataInicial, DateTime dataFinal)
        {
            int diasPadrão = 30;
            int totalDias;

            if (dataInicial == DateTime.MinValue) dataInicial = DateTime.Now.Date;
            if (dataFinal == DateTime.MinValue) totalDias = diasPadrão;

            DateTime dataInicialChecked = dataInicial;
            DateTime dataFinalChecked = dataFinal;

            var transacoes = bancoBdContext.Transacoes;
            List<Transacao> transacaoDoUser = new List<Transacao>();
            foreach (var transacao in transacoes)
            {
                if (transacao.ContaId == contaId)
                {
                    transacaoDoUser.Add(transacao);
                }
            }
            return transacaoDoUser;
        }

        public Transacao GetTransacao(int id)
        {
            return bancoBdContext.Transacoes.Find(id);
        }
    }
}
