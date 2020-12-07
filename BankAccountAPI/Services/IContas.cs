using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Services
{
    public interface IContas
    {
        IEnumerable<Conta> GetContas();
        Conta GetConta(int id);
        void CriarConta(Conta conta);
        void AtualizarConta(Conta conta);
        void DeletarConta(int id);
    }
}
