using System;

namespace ChillSite.SiteBlocks.Storage.FileStorage.Entities;

public class PageFileEntity
{
    public Guid PageId { get; set; }
    public string TemplateComponentTypeName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsPublished { get; set; }
    public DateTime? PublicationDate { get; set; }
    public string? SeoDescription { get; set; }
    public string? SeoKeywords { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }
}