using System;

namespace ChillSite.ContentBlocks.Common;

public interface IDateTimeService
{
    DateTime UtcNow { get; }
}