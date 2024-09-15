using System.IO;
using System.Threading.Tasks;
using Markdig;
using Microsoft.AspNetCore.Components;

namespace Stellaxis.SiteBlocks.Components;

public partial class SxMarkdownContent : ComponentBase
{
    [Parameter]
    public string? FilePath { get; set; }
    
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _content = await GetContentFromFragment();
    }

    private async Task<MarkupString> GetContentFromFragment()
    {
        
    }
    
    private async Task<MarkupString> GetContentFromFile(string filePath)
    {
        var markdownText = await File.ReadAllTextAsync(filePath);
        var html = Markdown.ToHtml(markdownText);
        return new MarkupString(html);
    }

    private MarkupString _content;
}