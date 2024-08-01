using ChillSite.SiteBlocks.Storage.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChillSite.SiteBlocks.Storage.Database;

public class SiteBlocksContext : DbContext
{
    public DbSet<PageEntity> Pages { get; set; } = null!;
}