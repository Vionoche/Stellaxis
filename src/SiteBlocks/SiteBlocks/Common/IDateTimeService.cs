using System;

namespace ChillSite.SiteBlocks.Common;

public interface IDateTimeService
{
    DateTime UtcNow { get; }
}