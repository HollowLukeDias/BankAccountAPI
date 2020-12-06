using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Services
{
    interface IContaOperacoes
    {
        void Depositar(Conta conta);
        void Saque(Conta conta);
        void Transferencia(Conta origem, Conta destino);
    }
}
