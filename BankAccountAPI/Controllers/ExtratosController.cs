using BankAccountAPI.Models;
using BankAccountAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Controllers
{
    [Route("api/extratos")]
    [ApiController]
    public class ExtratosController : ControllerBase
    {
        private IExtratos _extratos;

        public ExtratosController(IExtratos extratos)
        {
            _extratos = extratos;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var extratos = _extratos.GetExtratos();
            if (extratos == null) return BadRequest("Não existem extratos");
            return Ok(extratos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var extrato = _extratos.GetExtrato(id);
            if (extrato == null) return NotFound($"Não foi encontrado Extrato com ID: {id}");
            return Ok(extrato);
        }
    }
}
