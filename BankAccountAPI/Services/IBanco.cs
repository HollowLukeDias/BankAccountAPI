using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Services
{
    public interface IBanco
    {
        void DepositoGeraExtrato(Conta conta, float quantidade);
        void SaqueGeraExtrato(Conta conta, float quantidade);
        void TransferenciaGeraExtrato(Conta conta, Conta contaDestino, float quantidade);
    }
}
