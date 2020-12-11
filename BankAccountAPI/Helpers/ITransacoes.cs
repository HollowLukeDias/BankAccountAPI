using BankAccountAPI.Models;
using System.Collections.Generic;

namespace BankAccountAPI.Helpers
{
    public interface ITransacoes
    {
        IEnumerable<Transacao> GetTransacoes(int contaId, int page);
        Transacao GetTransacao(int idConta, int idTransacao);
    }
}
