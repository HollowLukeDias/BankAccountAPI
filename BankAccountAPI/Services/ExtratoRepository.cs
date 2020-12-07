using BankAccountAPI.Data;
using BankAccountAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Services
{
    public class ExtratoRepository : IExtratos
    {
        BancoDbContext bancoBdContext;

        public ExtratoRepository(BancoDbContext _bancoDbContext)
        {
            bancoBdContext = _bancoDbContext;
        }

        public IEnumerable<Extrato> GetExtratos()
        {
            return bancoBdContext.Extratos;
        }

        public Extrato GetExtrato(int id)
        {
            return bancoBdContext.Extratos.Find(id);
        }
    }
}
