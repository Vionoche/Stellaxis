using System;

namespace ChillSite.ContentBlocks.Pages;

public interface IPageManager
{
    Page Update(Page page, string title, string? description, string? seoDescription, string? seoKeywords);

    Page Publish(Page page, DateTime? publishedDate);

    Page Unpublish(Page page);
}