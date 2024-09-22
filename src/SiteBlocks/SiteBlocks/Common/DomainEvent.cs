using System;

namespace Stellaxis.SiteBlocks.Common;

public abstract record DomainEvent(
    Guid EventId,
    DateTime OccuredOn);