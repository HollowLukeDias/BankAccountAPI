using BankAccountAPI.Services;
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
        public IEnumerable<Conta> Get()
        {
            return _contasDb.GetContas();
        }

        // GET api/contas/5
        [HttpGet("{id}")]
        public Conta Get(int id)
        {
            return _contasDb.GetConta(id);
        }

        // POST api/contas
        [HttpPost]
        public void Post([FromBody] Conta conta)
        {
            _contasDb.CriarConta(conta);
        }

        // PUT api/contas/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Conta conta)
        {
            _contasDb.AtualizarConta(conta);
        }

        // DELETE api/contas/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _contasDb.DeletarConta(id);
        }

    }
}
