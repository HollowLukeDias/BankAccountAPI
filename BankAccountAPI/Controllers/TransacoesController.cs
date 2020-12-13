﻿using BankAccountAPI.Helpers;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Get(int id, int? page)
        {
            var checkPage = page ?? 1;
            var transacoes = _transacoes.GetTransacoes(id, checkPage);
            if (transacoes == null) return NotFound("Não existem Transações");
            return Ok(transacoes);
        }

        [HttpGet("{id}/transacoes/{idTransacao}")]
        public IActionResult Get(int idConta, int idTransacao)
        {
            var transacao = _transacoes.GetTransacao(idConta, idTransacao);
            if (transacao == null) return NotFound($"Não econtramos a Transação de ID: {idTransacao} na conta de ID: {idConta}");
            return Ok(transacao);
        }
    }
}
