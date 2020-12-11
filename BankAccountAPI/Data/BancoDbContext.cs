using BankAccountAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAccountAPI.Data
{
    public class BancoDbContext : DbContext
    {
        public BancoDbContext(DbContextOptions<BancoDbContext> options) : base(options) { }

        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<Conta> Contas { get; set; }
    }
}
