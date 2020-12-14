using BankAccountAPI.Models;
using System.Collections.Generic;

namespace BankAccountAPI.Helpers
{
    public interface IBanco
    {
        Transacao TransacaoDeposito(Conta conta, decimal quantidade);
        Transacao TransacaoSaque(Conta conta, decimal quantidade);
        Transacao TransacaoTransferencia(Conta conta, Conta contaDestino, decimal quantidade);
    }
}
