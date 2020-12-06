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

        private IContas contasDb;

        public ContasController(IContas _contasDb)
        {
            contasDb = _contasDb;
        }

        // GET: api/contas
        [HttpGet]
        public IEnumerable<Conta> Get()
        {
            return contasDb.GetContas();
        }

        // GET api/contas/5
        [HttpGet("{id}")]
        public Conta Get(int id)
        {
            return contasDb.GetConta(id);
        }

        // POST api/contas
        [HttpPost]
        public void Post([FromBody] Conta conta)
        {
            contasDb.CriarConta(conta);
        }

        // PUT api/contas/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Conta conta)
        {
            contasDb.AtualizarConta(conta);
        }

        // DELETE api/contas/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            contasDb.DeletarConta(id);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Conta conta, float saldo)
        {

        }

    }
}
