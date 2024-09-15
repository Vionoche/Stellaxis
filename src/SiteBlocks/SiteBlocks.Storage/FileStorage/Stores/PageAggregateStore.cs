using System;
using System.Threading;
using System.Threading.Tasks;
using Stellaxis.SiteBlocks.Common;
using Stellaxis.SiteBlocks.Pages.Aggregates;
using Stellaxis.SiteBlocks.Storage.FileStorage.Entities;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Stellaxis.SiteBlocks.Storage.FileStorage.Stores;

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
            TemplateComponentTypeName = page.TemplateComponentType.Name.Value,
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

    public Task<PageContentAggregate> GetPageContentBlocksAggregate(Guid pageId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SavePageContentBlocksAggregate(PageContentAggregate pageContentAggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IDomainEventBuffer _domainEventBuffer;
}