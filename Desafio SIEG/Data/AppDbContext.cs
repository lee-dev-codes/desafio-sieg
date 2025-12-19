using Microsoft.EntityFrameworkCore;
using Desafio_SIEG.Models;

namespace Desafio_SIEG.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<FiscalDocument> Documents => Set<FiscalDocument>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FiscalDocument>()
            .HasIndex(d => d.XmlHash)
            .IsUnique();
    }
}

