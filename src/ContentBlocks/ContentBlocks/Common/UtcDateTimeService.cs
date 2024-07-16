using System;

namespace ChillSite.ContentBlocks.Common;

public class UtcDateTimeService : IDateTimeService
{
    public DateTime UtcNow => DateTime.UtcNow;
}