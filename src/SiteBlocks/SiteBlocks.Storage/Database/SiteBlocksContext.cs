using Microsoft.EntityFrameworkCore;
using Stellaxis.SiteBlocks.Storage.Database.Entities;

namespace Stellaxis.SiteBlocks.Storage.Database;

public class SiteBlocksContext : DbContext
{
    public DbSet<PageEntity> Pages { get; set; } = null!;
}