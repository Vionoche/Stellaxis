using System;

namespace Stellaxis.SiteBlocks.Common;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}