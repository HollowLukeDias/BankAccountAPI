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

        private IContas _contas;
        private IBanco _bancoDb;

        public OperacoesController(IBanco bancoDb, IContas contas)
        {
            _contas = contas;
            _bancoDb = bancoDb;
        }

        [HttpPut("deposito/{id}")]
        public IActionResult Deposito(int id, float quantidade)
        {
            var conta = _contas.GetConta(id);
            if (conta == null) return BadRequest($"Não foi encontrado uma conta de ID: {id}");
            if (quantidade <= 0) return BadRequest($"O valor {quantidade:F2} não é aceito");
            _bancoDb.DepositoGeraExtrato(conta, quantidade);
            return Ok($"Tentativa de deposito efeutada, por favor verifique os extratos");
        }

        [HttpPut("saque/{id}")]
        public IActionResult Saque(int id, float quantidade)
        {
            var conta = _contas.GetConta(id);
            if (conta == null) return BadRequest($"Não foi encontrado uma conta de ID: {id}");
            if (quantidade <= 0) return BadRequest($"O valor {quantidade:F2} não é aceito");
            _bancoDb.SaqueGeraExtrato(conta, quantidade);
            return Ok("Tentativa de saque efetuada, por favor verifique os extratos");
        }

        [HttpPut("transferencia/{id}")]
        public IActionResult Transferencia(int id, float quantidade, int idDestino)
        {
            var conta = _contas.GetConta(id);
            var contaDestino = _contas.GetConta(idDestino);
            if (conta == null) return BadRequest($"Não foi encontrado uma conta de Origem de ID: {id}");
            if (contaDestino == null) return BadRequest($"Não foi encontrado uma conta de Destino de ID: {idDestino}");
            if (quantidade <= 0) return BadRequest($"O valor {quantidade:F2} não é aceito");
            _bancoDb.TransferenciaGeraExtrato(conta, contaDestino, quantidade);
            return Ok($"Tentativa de transferência efetuada, por favor verifique os extratos");
        }
    }
}
