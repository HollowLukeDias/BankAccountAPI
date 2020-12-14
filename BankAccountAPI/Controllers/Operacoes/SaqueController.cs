using BankAccountAPI.Data;
using BankAccountAPI.Helpers;
using BankAccountAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountAPI.Controllers
{
    [Route("api/contas")]
    [ApiController]
    public class SaqueController : ControllerBase
    {
        private readonly IRepository<Conta> _contaRepository;
        private readonly IBanco _bancoDb;

        public SaqueController(IBanco bancoDb, BancoDbContext bancoDbContext)
        {
            _contaRepository = new Repository<Conta>(bancoDbContext);
            _bancoDb = bancoDb;
        }

        [HttpPost("{id}/saque")]
        public IActionResult Saque(int id, decimal valor)
        {
            if (valor <= 0) return BadRequest($"O valor {valor:F2} é inválido.");

            var conta = _contaRepository.Get(id);
            if (conta == null) return NotFound($"Não existe conta com o ID: {id} registrada no Banco de Dados.");

            var transacao = _bancoDb.TransacaoSaque(conta, valor);
            return Ok(transacao);
        }
    }
}
