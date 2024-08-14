using System.Text;
using ChillSite.SiteBlocks.Common;
using ChillSite.SiteBlocks.Pages;
using ChillSite.SiteBlocks.Storage.FileStorage.Stores;

namespace SiteBlocks.Storage.IntegrationTests.FileStorage;

public class PageStoreTests
{
    [Fact]
    public async Task Test_Page_Saving()
    {
        var mainLayout = new LayoutComponentType(
            new LayoutComponentName("MainLayout"),
            typeof(object));

        var pageTemplate = new TemplateComponentType(
            new TemplateComponentName("HomePage"),
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

        var store = new PageStore();

        await store.SavePage(page, default);
    }
    
    private readonly IDateTimeProvider _dateTimeProvider = new UtcDateTimeProvider();
    private readonly IDomainEventBuffer _domainEventBuffer = new InMemoryDomainEventBuffer(new UtcDateTimeProvider());
}