using BankAccountAPI.Data;
using BankAccountAPI.Helpers;
using BankAccountAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountAPI.Controllers
{
    [Route("api/contas")]
    [ApiController]
    public class DepositoController : ControllerBase
    {
        private IRepository<Conta> _contaRepository;
        private IBanco _bancoDb;

        public DepositoController(IBanco bancoDb, BancoDbContext bancoDbContext)
        {
            _contaRepository = new Repository<Conta>(bancoDbContext);
            _bancoDb = bancoDb;
        }

        [HttpPost("{id}/deposito")]
        public IActionResult Deposito(int id, decimal valor)
        {
            if (valor <= 0) return BadRequest($"O valor {valor:F2} não é aceito");

            var conta = _contaRepository.Get    (id);
            if (conta == null) return NotFound($"Não foi encontrado uma conta de ID: {id}");

            _bancoDb.TransacaoDeposito(conta, valor);
            return Ok("Tentativa de deposito efeutada, por favor verifique o Extrato");
        }
    }
}
