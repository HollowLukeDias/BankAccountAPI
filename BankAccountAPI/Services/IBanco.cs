using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Services
{
    interface IBanco
    {
        void Depositar(Conta conta, float quantidade);
        void Saque(Conta conta, float quantidade);
        void Transferencia(Conta origem, Conta destino, float quantidade);
    }
}
