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
        private IExtratos _extratosDb;

        public ExtratosController(IExtratos extratos)
        {
            _extratosDb = extratos;
        }

        [HttpGet]
        public IEnumerable<Extrato> Get()
        {
            return _extratosDb.GetExtratos();
        }

        [HttpGet("{id}")]
        public Extrato Get(int id)
        {
            return _extratosDb.GetExtrato(id);
        }
    }
}
