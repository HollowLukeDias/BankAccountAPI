using BankAccountAPI.Models;
using BankAccountAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Controllers
{
    [Route("api/contas")]
    [ApiController]
    public class TransacoesController : ControllerBase
    {
        private ITransacoes _transacoes;

        public TransacoesController(ITransacoes transacoes)
        {
            _transacoes = transacoes;
        }

        [HttpGet("{id}/transacoes")]
        public IActionResult Get(int id, string dataStringInicial, string dataStringFinal)
        {
            DateTime dataInicial;
            DateTime dataFinal;
            try
            { 
                DateTime.TryParse(dataStringInicial, out dataInicial);
                DateTime.TryParse(dataStringFinal, out dataFinal);
            }
            catch
            {
                return BadRequest("DATA INFORMADA NÃO É VÁLIDA");
            }
            var transacoes = _transacoes.GetTransacoes(id, dataInicial, dataFinal);
            if (transacoes == null) return NotFound("Não existem Transações");
            return Ok(transacoes);
        }

        [HttpGet("{id}/transacoes/{idTransacao}")]
        public IActionResult Get(int id, int idTranscao)
        {
            var transacao = _transacoes.GetTransacao(id);
            if (transacao == null) return NotFound($"Não foram encontradas Transações com ID: {id}");
            return Ok(transacao);
        }
    }
}
