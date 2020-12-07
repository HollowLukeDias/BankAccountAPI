using BankAccountAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankAccountAPI.Controllers
{
    [Route("api/operacao")]
    [ApiController]
    public class OperacoesController : ControllerBase
    {

        private IContas _contasDb;

        public OperacoesController(IContas contasDb)
        {
            _contasDb = contasDb;
        }

        [HttpPut("deposito/{id}")]
        public void Put(int id, [FromBody]  Conta conta, float quantidade)
        {

        }
    }
}
