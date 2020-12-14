using BankAccountAPI.Data;
using BankAccountAPI.Helpers;
using BankAccountAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountAPI.Controllers
{
    [Route("api/contas")]
    [ApiController]
    public class ContasController : ControllerBase
    {

        private readonly IRepository<Conta> _contaRepository;

        public ContasController(BancoDbContext bancoDb)
        {
            _contaRepository = new Repository<Conta>(bancoDb);
        }

        // GET: api/contas
        [HttpGet]
        public IActionResult Get()
        {
            var contas = _contaRepository.GetAll();
            if (contas == null) return NotFound("Não existem contas cadastradas nesse Banco de Dados.");
            return Ok(contas);
        }

        // GET api/contas/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var conta = _contaRepository.Get(id);
            if (conta == null) return NotFound($"Não existe conta com o ID: {id} registrada no Banco de Dados.");
            return Ok(conta);
        }

        // POST api/contas
        [HttpPost]
        public IActionResult Post([FromBody] Conta conta)
        {
            if (ModelState.IsValid)
            {
                _contaRepository.Add(conta);
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest(ModelState);
        }

        // PUT api/contas/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Conta conta)
        {
            if (id != conta.Id) return BadRequest($"O ID enviado: {id} é diferente do ID da conta: {conta.Id}");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                _contaRepository.Update(conta);
                return Ok($"A Conta de ID: {id} foi atualizada");
            }
            catch
            {
                return NotFound($"NNão existe conta com o ID: {id} registrada no Banco de Dados.");
            }
        }

        // DELETE api/contas/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var conta = _contaRepository.Get(id);
                _contaRepository.Remove(conta);
                return Ok($"A Conta com ID: {id} foi deletada");
            }
            catch
            {
                return NotFound($"Não existe conta com o ID: {id} registrada no Banco de Dados.");
            }
        }

    }
}
