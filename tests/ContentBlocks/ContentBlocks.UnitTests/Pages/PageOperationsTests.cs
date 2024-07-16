using System;
using ChillSite.ContentBlocks.Common;
using ChillSite.ContentBlocks.Pages;
using FluentAssertions;
using Xunit;

namespace ChillSite.ContentBlocks.UnitTests.Pages;

public class PageOperationsTests
{
    [Fact]
    public void Test_Page_Creation_And_Publication_With_Pure_Operations()
    {
        var page = Page.Create(
            _dateTimeService,
            _domainEventHandler,
            title: "Home",
            description: "The main page",
            seoDescription: null,
            seoKeywords: null);

        page = page
            .Update(_dateTimeService, _domainEventHandler, "Home", "Home page", "Welcome site", null)
            .Publish(_dateTimeService, _domainEventHandler);
        
        page.PageId.Should().NotBeEmpty();
        page.Title.Should().Be("Home");
        page.Description.Should().Be("Home page");
        page.SeoDescription.Should().Be("Welcome site");
        page.SeoKeywords.Should().BeNull();
        page.CreationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
        page.ModificationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
    }
    
    [Fact]
    public void Test_Page_Creation_And_Publication_With_Pure_Manager()
    {
        var pageManager = new PageManager(_dateTimeService, _domainEventHandler);
        
        var page = Page.Create(
            _dateTimeService,
            _domainEventHandler,
            title: "Home",
            description: "The main page",
            seoDescription: null,
            seoKeywords: null);

        page = pageManager.Update(page, "Home", "Home page", "Welcome site", null);
        page = pageManager.Publish(page);
        
        page.PageId.Should().NotBeEmpty();
        page.Title.Should().Be("Home");
        page.Description.Should().Be("Home page");
        page.SeoDescription.Should().Be("Welcome site");
        page.SeoKeywords.Should().BeNull();
        page.CreationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
        page.ModificationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
    }
    
    private readonly IDateTimeService _dateTimeService = new UtcDateTimeService();
    private readonly IDomainEventHandler _domainEventHandler = new MemoryDomainEventHandler(new UtcDateTimeService());
}