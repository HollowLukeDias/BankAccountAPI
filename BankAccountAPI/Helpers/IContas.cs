using System.Collections.Generic;

namespace BankAccountAPI.Helpers
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
