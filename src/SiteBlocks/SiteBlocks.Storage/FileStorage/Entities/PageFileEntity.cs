using System;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Stellaxis.SiteBlocks.Storage.FileStorage.Entities;

public class PageFileEntity
{
    public Guid PageId { get; set; }
    
    public string TemplateComponentTypeName { get; set; } = null!;
    
    public string Name { get; set; } = null!;
    
    public string Title { get; set; } = null!;
    
    [YamlMember(ScalarStyle = ScalarStyle.Literal)]
    public string? Description { get; set; }
    
    public bool IsPublished { get; set; }
    
    public DateTime? PublicationDate { get; set; }
    
    public string? SeoDescription { get; set; }
    
    public string? SeoKeywords { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public DateTime? ModificationDate { get; set; }
}