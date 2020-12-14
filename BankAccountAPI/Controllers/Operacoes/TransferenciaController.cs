using BankAccountAPI.Data;
using BankAccountAPI.Helpers;
using BankAccountAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountAPI.Controllers
{
    [Route("api/contas")]
    [ApiController]
    public class TransferenciaController : ControllerBase
    {
        private IRepository<Conta> _contaRepository;
        private IBanco _bancoDb;

        public TransferenciaController(IBanco bancoDb, BancoDbContext bancoDbContext)
        {
            _bancoDb = bancoDb;
            _contaRepository = new Repository<Conta>(bancoDbContext);
        }



        [HttpPost("{id}/transferencia/{idDestino}")]
        public IActionResult Transferencia(int id, int idDestino, decimal valor)
        {
            if (valor <= 0) return BadRequest($"O valor {valor:F2} é inválido.");

            var conta = _contaRepository.Get(id);
            if (conta == null) return NotFound($"Não existe conta com o ID: {id} registrada no Banco de Dados.");

            var contaDestino = _contaRepository.Get(idDestino);
            if (contaDestino == null) return NotFound($"Não existe conta com o ID: {idDestino} registrada no Banco de Dados\n" +
                                                      $"Revise o ID da conta que deseja transferir para.");

            var transacao = _bancoDb.TransacaoTransferencia(conta, contaDestino, valor);
            return Ok(transacao);
        }
    }
}
