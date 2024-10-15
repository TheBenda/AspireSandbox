using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AKS.Infrastructure.Data;

public class PrimaryContextDbContextFactory : IDesignTimeDbContextFactory<PrimaryDbContext>
{
    public PrimaryDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PrimaryDbContext>();
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=dummy-data-source;User Id=dotnetuser;Password=supersecretpw;");

        return new PrimaryDbContext(optionsBuilder.Options);
    }
}
