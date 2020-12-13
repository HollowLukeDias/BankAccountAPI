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
        public IActionResult Transferencia(int id, int idDestino, decimal quantidade)
        {
            if (quantidade <= 0) return BadRequest($"O valor {quantidade:F2} não é aceito");

            var conta = _contaRepository.Get(id);
            if (conta == null) return NotFound($"Não foi encontrado uma conta de Origem de ID: {id}");

            var contaDestino = _contaRepository.Get(idDestino);
            if (contaDestino == null) return NotFound($"Não foi encontrado uma conta de Destino de ID: {idDestino}");

            _bancoDb.TransacaoTransferencia(conta, contaDestino, quantidade);
            return Ok($"Tentativa de transferência efetuada, por favor verifique o Extrato");
        }
    }
}
