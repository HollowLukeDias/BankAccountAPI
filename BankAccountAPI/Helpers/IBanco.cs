using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Helpers
    public interface IBanco
    {
        void TransacaoDeposito(Conta conta, float quantidade);
        void TransacaoSaque(Conta conta, float quantidade);
        void TransacaoTransferencia(Conta conta, Conta contaDestino, float quantidade);
    }
}
