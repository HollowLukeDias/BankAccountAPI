using BankAccountAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Controllers
{
    [Route("api/operacao")]
    [ApiController]
    public class OperacoesController : ControllerBase
    {

        private IBanco _bancoDb;

        public OperacoesController(IBanco bancoDb)
        {
            _bancoDb = bancoDb;
        }

        [HttpPut("deposito/{id}")]
        public void Deposito(int id, float quantidade)
        {
            _bancoDb.Depositar(id, quantidade);
        }

        [HttpPut("saque/{id}")]
        public void Saque(int id, float quantidade)
        {
            _bancoDb.Saque(id, quantidade);
        }
    }
}
