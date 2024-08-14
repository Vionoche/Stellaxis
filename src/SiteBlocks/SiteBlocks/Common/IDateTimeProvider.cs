using System;

namespace ChillSite.SiteBlocks.Common;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}