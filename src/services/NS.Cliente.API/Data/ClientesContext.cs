using NS.Core.Data;
using NS.Clientes.API.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NS.Clientes.API.Data;

public sealed class ClientesContext : DbContext, IUnitOfWork
{
    public ClientesContext(DbContextOptions<ClientesContext> options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();

        foreach (IMutableProperty property in modelBuilder.Model.GetEntityTypes().SelectMany(
                     e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        foreach (IMutableForeignKey relationship in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientesContext).Assembly);
    }

    public async Task<bool> Commit()
    {
        bool sucesso = await base.SaveChangesAsync() > 0;

        return sucesso;
    }
}