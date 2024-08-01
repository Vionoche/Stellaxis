using System;
using System.Linq;
using ChillSite.SiteBlocks.Common;
using ChillSite.SiteBlocks.Pages;
using FluentAssertions;
using Xunit;

namespace ChillSite.SiteBlocks.UnitTests.Pages;

public class PageOperationsTests
{
    [Fact]
    public void Test_Page_Creation_And_Publication_With_Pure_Operations()
    {
        var mainLayout = new LayoutComponentType(
            new LayoutComponentName("MainLayout"),
            typeof(object));

        var pageTemplate = new TemplateComponentType(
            new TemplateComponentName("HomePage"),
            typeof(object),
            mainLayout);
        
        var page = Page.Create(
            _dateTimeService,
            _domainEventHandler,
            pageTemplate,
            name: "Home",
            title: "Home",
            description: "The main page",
            seoDescription: null,
            seoKeywords: null);

        page = page
            .Update(_dateTimeService, _domainEventHandler, "Home", "Home page", "Welcome site", null)
            .Publish(_dateTimeService, _domainEventHandler);
        
        page.PageId.Should().NotBeEmpty();
        page.TemplateComponentType.Should().Be(pageTemplate);
        page.Title.Should().Be("Home");
        page.Description.Should().Be("Home page");
        page.SeoDescription.Should().Be("Welcome site");
        page.SeoKeywords.Should().BeNull();
        page.CreationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
        page.ModificationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
    }

    [Fact]
    public void Test_Page_Name_Validation()
    {
        var mainLayout = new LayoutComponentType(
            new LayoutComponentName("MainLayout"),
            typeof(object));

        var pageTemplate = new TemplateComponentType(
            new TemplateComponentName("HomePage"),
            typeof(object),
            mainLayout);
        
        var page = Page.Create(
            _dateTimeService,
            _domainEventHandler,
            pageTemplate,
            name: "Home",
            title: "Home",
            description: "The main page",
            seoDescription: null,
            seoKeywords: null);

        page.UpdateName(_dateTimeService, _domainEventHandler, "Home01");
        page.UpdateName(_dateTimeService, _domainEventHandler, "Home_Page");
        page.UpdateName(_dateTimeService, _domainEventHandler, "Home-Page");
        page.UpdateName(_dateTimeService, _domainEventHandler, "Главная");
        page.UpdateName(_dateTimeService, _domainEventHandler, "Главная01");
        page.UpdateName(_dateTimeService, _domainEventHandler, "Главная_Страница");
        page.UpdateName(_dateTimeService, _domainEventHandler, "Главная-Страница");
        
       var action = () =>  page.UpdateName(_dateTimeService, _domainEventHandler, "Home 01");
       action.Should().Throw<Exception>();
       
       action = () =>  page.UpdateName(_dateTimeService, _domainEventHandler, "Home's Page");
       action.Should().Throw<Exception>();
       
       action = () =>  page.UpdateName(_dateTimeService, _domainEventHandler, "Home!");
       action.Should().Throw<Exception>();
       
       action = () =>  page.UpdateName(_dateTimeService, _domainEventHandler, "Home%");
       action.Should().Throw<Exception>();
       
       action = () =>  page.UpdateName(_dateTimeService, _domainEventHandler, "");
       action.Should().Throw<Exception>();
       
       action = () =>  page.UpdateName(_dateTimeService, _domainEventHandler,
           name: new string(Enumerable.Range(0, 30).Select(_ => 'w').ToArray()));
       action.Should().Throw<Exception>();
    }
    
    private readonly IDateTimeService _dateTimeService = new UtcDateTimeService();
    private readonly IDomainEventHandler _domainEventHandler = new MemoryDomainEventHandler(new UtcDateTimeService());
}