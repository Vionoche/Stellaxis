using System;

namespace Stellaxis.SiteBlocks.Common;

public class UtcDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}