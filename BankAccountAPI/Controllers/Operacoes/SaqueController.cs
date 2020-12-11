﻿using BankAccountAPI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountAPI.Controllers
{
    [Route("api/contas")]
    [ApiController]
    public class SaqueController : ControllerBase
    {
        private IContas _contas;
        private IBanco _bancoDb;

        public SaqueController(IBanco bancoDb, IContas contas)
        {
            _contas = contas;
            _bancoDb = bancoDb;
        }

        [HttpPost("{id}/saque")]
        public IActionResult Saque(int id, decimal quantidade)
        {
            var conta = _contas.GetConta(id);
            if (conta == null) return NotFound($"Não foi encontrado uma conta de ID: {id}");
            if (quantidade <= 0) return BadRequest($"O valor {quantidade:F2} não é aceito");
            _bancoDb.TransacaoSaque(conta, quantidade);
            return Ok("Tentativa de saque efetuada, por favor verifique o Extrato");
        }
    }
}
