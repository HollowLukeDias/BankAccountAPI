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

        public IEnumerable<Transacao> GetTransacoes()
        {
            return bancoBdContext.Transacoes;
        }

        public Transacao GetTransacao(int id)
        {
            return bancoBdContext.Transacoes.Find(id);
        }
    }
}
