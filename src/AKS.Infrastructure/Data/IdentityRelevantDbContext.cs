using AKS.Domain.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

namespace AKS.Infrastructure.Data;

public class IdentityRelevantDbContext(DbContextOptions<IdentityRelevantDbContext> options) :
    IdentityDbContext<User>(options)
{
}
