using System;

namespace ChillSite.SiteBlocks.Common;

public class UtcDateTimeService : IDateTimeService
{
    public DateTime UtcNow => DateTime.UtcNow;
}