using Microsoft.EntityFrameworkCore;

namespace AKS.Infrastructure.Data;

public class PrimaryDbContext(DbContextOptions<PrimaryDbContext> dbContextOptions) : DbContext(dbContextOptions)
{

}
