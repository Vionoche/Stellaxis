using System;

namespace ChillSite.SiteBlocks.Common;

public class UtcDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}