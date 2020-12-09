using BankAccountAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Helpers
{
    public class ContasRepository : IContas
    {
        BancoDbContext bancoDbContext;

        public ContasRepository(BancoDbContext _bancoDbContext)
        {
            bancoDbContext = _bancoDbContext;
        }

        public IEnumerable<Conta> GetContas()
        {
            return bancoDbContext.Contas;
        }

        public Conta GetConta(int id)
        {
            var conta = bancoDbContext.Contas.Find(id);
            return conta;
        }

        public void CriarConta(Conta conta)
        {
            bancoDbContext.Contas.Add(conta);
            bancoDbContext.SaveChanges(true);
        }

        public void AtualizarConta(Conta conta)
        {
            bancoDbContext.Contas.Update(conta);
            bancoDbContext.SaveChanges(true);
        }

        public void DeletarConta(int id)
        {
            var product = bancoDbContext.Contas.Find(id);
            bancoDbContext.Contas.Remove(product);
            bancoDbContext.SaveChanges(true);
        }

    }
}
