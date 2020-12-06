using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAPI.Data
{
    public class ContasDbContext : DbContext
    {

        public ContasDbContext(DbContextOptions<ContasDbContext> options) : base(options) { }

        public DbSet<Conta> Products { get; set; }
    }
}
