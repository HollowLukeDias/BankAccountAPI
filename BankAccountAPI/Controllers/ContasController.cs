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

        // GET: api/conta
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "sua mãe", "de quatro" };
        }

        // GET api/<ContasController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ContasController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ContasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ContasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
