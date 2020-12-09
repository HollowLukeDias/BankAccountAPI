using BankAccountAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositoController : ControllerBase
    {
        private IContas _contas;
        private IBanco _bancoDb;

        public DepositoController(IBanco bancoDb, IContas contas)
        {
            _contas = contas;
            _bancoDb = bancoDb;
        }

        [HttpPost("{id}/deposito")]
        public IActionResult Deposito(int id, float quantidade)
        {
            var conta = _contas.GetConta(id);
            if (conta == null) return NotFound($"Não foi encontrado uma conta de ID: {id}");
            if (quantidade <= 0) return BadRequest($"O valor {quantidade:F2} não é aceito");
            _bancoDb.TransacaoDeposito(conta, quantidade);
            return Ok($"Tentativa de deposito efeutada, por favor verifique os Extrato");
        }
    }
}
