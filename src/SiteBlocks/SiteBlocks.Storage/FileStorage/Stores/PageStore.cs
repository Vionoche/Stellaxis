using System;
using System.Threading;
using System.Threading.Tasks;
using ChillSite.SiteBlocks.Pages;
using ChillSite.SiteBlocks.Storage.FileStorage.Entities;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ChillSite.SiteBlocks.Storage.FileStorage.Stores;

public class PageStore : IPageStore
{
    public Task<Page> GetPage(Guid pageId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SavePage(Page page, CancellationToken cancellationToken)
    {
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
            .WithNamingConvention(PascalCaseNamingConvention.Instance)
            .Build();
        
        var yaml = serializer.Serialize(pageEntity);
        
        // todo: save to file
        
        return Task.CompletedTask;
    }
}