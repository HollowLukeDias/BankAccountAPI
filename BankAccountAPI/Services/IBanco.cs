using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Services
{
    public interface IBanco
    {
        void Depositar(int id, float quantidade);
        void Saque(int id, float quantidade);
        void Transferencia(Conta origem, Conta destino, float quantidade);
    }
}
