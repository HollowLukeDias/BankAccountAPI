using BankAccountAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Data
{
    public class ExtratoDbContext : DbContext
    {
        public ExtratoDbContext(DbContextOptions<ContasDbContext> options) : base(options) { }

        public DbSet<Extrato> Extratos { get; set; }
    }
}
