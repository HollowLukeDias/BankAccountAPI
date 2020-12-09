using BankAccountAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Helpers
{
    public interface ITransacoes
    {
        IEnumerable<Transacao> GetTransacoes(int contaId, DateTime dataInicial, DateTime dataFinal);
        Transacao GetTransacao(int id);
    }
}
