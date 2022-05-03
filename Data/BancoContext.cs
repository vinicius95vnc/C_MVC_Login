using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_Login.Models;

namespace MVC_Login.Data
{
    public class BancoContext : IdentityDbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options)
            : base(options)
        {
        }

        public DbSet<Participantes>? Participantes { get; set; }
    }
}