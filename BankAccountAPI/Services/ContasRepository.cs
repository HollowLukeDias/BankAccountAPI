using BankAccountAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Services
{
    public class ContasRepository : IContas
    {
        ContasDbContext contasDbContext;

        public ContasRepository(ContasDbContext _contasDbContext)
        {
            contasDbContext = _contasDbContext;
        }

        public IEnumerable<Conta> GetContas()
        {
            return contasDbContext.Contas;
        }

        public Conta GetConta(int id)
        {
            var conta = contasDbContext.Contas.Find(id);
            return conta;
        }

        public void CriarConta(Conta conta)
        {
            contasDbContext.Contas.Add(conta);
            contasDbContext.SaveChanges(true);
        }

        public void AtualizarConta(Conta conta)
        {
            contasDbContext.Contas.Update(conta);
            contasDbContext.SaveChanges(true);
        }

        public void DeletarConta(int id)
        {
            var product = contasDbContext.Contas.Find(id);
            contasDbContext.Contas.Remove(product);
            contasDbContext.SaveChanges(true);
        }
    }
}
