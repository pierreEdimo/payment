using Microsoft.EntityFrameworkCore;
using payment.model;

namespace payment.DatabaseContext;

public class PayMentDbContext : DbContext
{
    public PayMentDbContext(DbContextOptions<PayMentDbContext> options) : base(options)
    {
    }

    public DbSet<Wallet>? Wallets { get; set; }
    public DbSet<Activity>? Activities{get; set;}
}