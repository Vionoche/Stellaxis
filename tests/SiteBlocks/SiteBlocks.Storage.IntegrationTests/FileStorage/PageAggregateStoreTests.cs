using System.Text;
using System.Threading.Tasks;
using Stellaxis.SiteBlocks.Common;
using Stellaxis.SiteBlocks.Pages;
using Stellaxis.SiteBlocks.Pages.Aggregates;
using Stellaxis.SiteBlocks.Storage.FileStorage.Stores;
using Xunit;

namespace SiteBlocks.Storage.IntegrationTests.FileStorage;

public class PageAggregateStoreTests
{
    [Fact]
    public async Task Test_PageAggregate_Saving()
    {
        var mainLayout = new LayoutComponentType(
            new LayoutComponentTypeName("MainLayout"),
            typeof(object));

        var pageTemplate = new TemplateComponentType(
            new TemplateComponentTypeName("HomePage"),
            typeof(object),
            mainLayout);

        var descriptionBuilder = new StringBuilder();

        descriptionBuilder = descriptionBuilder
            .AppendLine("First line")
            .AppendLine("Second line")
            .AppendLine("Third line");
        
        var page = Page.Create(
            _dateTimeProvider,
            _domainEventBuffer,
            pageTemplate,
            name: "Home",
            title: "Home",
            description: descriptionBuilder.ToString(),
            seoDescription: null,
            seoKeywords: null);

        var pageAggregate = new PageAggregate(_dateTimeProvider, _domainEventBuffer, page);

        var store = new PageAggregateStore(_dateTimeProvider, _domainEventBuffer);

        await store.SavePageAggregate(pageAggregate, default);
    }
    
    private readonly IDateTimeProvider _dateTimeProvider = new UtcDateTimeProvider();
    private readonly IDomainEventBuffer _domainEventBuffer = new InMemoryDomainEventBuffer(new UtcDateTimeProvider());
}