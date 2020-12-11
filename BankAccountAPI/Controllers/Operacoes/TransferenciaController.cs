using BankAccountAPI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountAPI.Controllers
{
    [Route("api/contas")]
    [ApiController]
    public class TransferenciaController : ControllerBase
    {
        private IContas _contas;
        private IBanco _bancoDb;

        public TransferenciaController(IBanco bancoDb, IContas contas)
        {
            _contas = contas;
            _bancoDb = bancoDb;
        }



        [HttpPost("{id}/transferencia/{idDestino}")]
        public IActionResult Transferencia(int id, int idDestino, decimal quantidade)
        {
            var conta = _contas.GetConta(id);
            var contaDestino = _contas.GetConta(idDestino);
            if (conta == null) return NotFound($"Não foi encontrado uma conta de Origem de ID: {id}");
            if (contaDestino == null) return NotFound($"Não foi encontrado uma conta de Destino de ID: {idDestino}");
            if (quantidade <= 0) return BadRequest($"O valor {quantidade:F2} não é aceito");
            _bancoDb.TransacaoTransferencia(conta, contaDestino, quantidade);
            return Ok($"Tentativa de transferência efetuada, por favor verifique o Extrato");
        }
    }
}
