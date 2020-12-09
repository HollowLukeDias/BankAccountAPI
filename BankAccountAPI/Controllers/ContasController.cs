using BankAccountAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Controllers
{
    [Route("api/contas")]
    [ApiController]
    public class ContasController : ControllerBase
    {

        private IContas _contasDb;

        public ContasController(IContas contasDb)
        {
            _contasDb = contasDb;
        }

        // GET: api/contas
        [HttpGet]
        public IActionResult Get()
        {
            var contas = _contasDb.GetContas();
            if (contas == null) return BadRequest("Não existem contas cadastradas");
            return Ok(contas);
        }

        // GET api/contas/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var conta = _contasDb.GetConta(id);
            if (conta == null) return BadRequest($"Não existe conta com esse ID: {id}");
            return Ok(conta);
        }

        // POST api/contas
        [HttpPost]
        public IActionResult Post([FromBody] Conta conta)
        {

            if (ModelState.IsValid)
            {
                _contasDb.CriarConta(conta);
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest(ModelState);
        }

        // PUT api/contas/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Conta conta)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != conta.Id) return BadRequest($"O ID enviado: {id} é diferente do ID da conta: {conta.Id}");

            try
            {
                _contasDb.AtualizarConta(conta);
                return Ok($"Conta de ID: {id} atualizada");
            }
            catch (Exception)
            {
                return NotFound($"Não existe conta com o ID: {id}");
            }
        }

        // DELETE api/contas/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _contasDb.DeletarConta(id);
                return Ok($"Conta com ID: {id} deletada");
            }
            catch (Exception)
            {
                return NotFound($"Não foi encontrada uma conta com o ID: {id}");
            }
        }

    }
}
