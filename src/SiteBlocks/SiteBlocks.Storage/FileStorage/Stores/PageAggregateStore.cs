using System;
using System.Threading;
using System.Threading.Tasks;
using ChillSite.SiteBlocks.Common;
using ChillSite.SiteBlocks.Pages.Aggregates;
using ChillSite.SiteBlocks.Storage.FileStorage.Entities;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ChillSite.SiteBlocks.Storage.FileStorage.Stores;

public class PageAggregateStore : IPageAggregateStore
{
    public PageAggregateStore(
        IDateTimeProvider dateTimeProvider,
        IDomainEventBuffer domainEventBuffer)
    {
        _dateTimeProvider = dateTimeProvider;
        _domainEventBuffer = domainEventBuffer;
    }
    
    public Task<PageAggregate> GetPageAggregate(Guid pageId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SavePageAggregate(PageAggregate pageAggregate, CancellationToken cancellationToken)
    {
        var page = pageAggregate.Page;
        
        var pageEntity = new PageFileEntity
        {
            PageId = page.PageId,
            TemplateComponentTypeName = page.TemplateComponentType.ComponentName.Name,
            Name = page.Name,
            Title = page.Title,
            Description = page.Description,
            IsPublished = page.IsPublished,
            PublicationDate = page.PublicationDate,
            SeoDescription = page.SeoDescription,
            SeoKeywords = page.SeoKeywords,
            CreationDate = page.CreationDate,
            ModificationDate = page.ModificationDate
        };

        var serializer = new SerializerBuilder()
            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        
        var yaml = serializer.Serialize(pageEntity);
        
        // todo: save to file
        
        return Task.CompletedTask;
    }

    public Task<PageContentBlocksAggregate> GetPageContentBlocksAggregate(Guid pageId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SavePageContentBlocksAggregate(PageContentBlocksAggregate pageContentBlocksAggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IDomainEventBuffer _domainEventBuffer;
}