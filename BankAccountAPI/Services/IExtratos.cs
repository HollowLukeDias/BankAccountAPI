using BankAccountAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Services
{
    public interface IExtratos
    {
        IEnumerable<Extrato> GetExtratos();
        Extrato GetExtrato(int id);
    }
}
