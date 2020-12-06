using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace BankAccountAPI
{
    public class Conta
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public SecureString SenhaCliente { get; set; }
        public int Saldo { get; set; }
    }
}
