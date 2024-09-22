using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Markdig;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Stellaxis.SiteBlocks.Components;

public partial class SxMarkdownContent : ComponentBase
{
    [Parameter]
    public Func<string>? FilePathFunc { get; set; }
    
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (ChildContent != null)
        {
            _content = GetContentFromFragment();
        }
        else if (FilePathFunc != null)
        {
            _content = await GetContentFromFile(FilePathFunc());
        }
    }

    private MarkupString GetContentFromFragment()
    {
        if (ChildContent == null)
        {
            return new MarkupString();
        }

        using var builder = new RenderTreeBuilder();
        ChildContent(builder);
        
        var framesRange = builder.GetFrames();

#pragma warning disable BL0006
        
        var textContent = framesRange.Array
            .Where(x => x.FrameType is RenderTreeFrameType.Markup or RenderTreeFrameType.Text)
            .Aggregate(new StringBuilder(), (sb, frame) => sb.AppendLine(frame.TextContent));
        
#pragma warning restore BL0006

        var markdown = DetectIndentsRegex().Replace(textContent.ToString(), "\r\n");
        var html = Markdown.ToHtml(markdown);
        
        return new MarkupString(html);
    }
    
    private static async Task<MarkupString> GetContentFromFile(string filePath)
    {
        var markdownText = await File.ReadAllTextAsync(filePath);
        var html = Markdown.ToHtml(markdownText);
        return new MarkupString(html);
    }

    private MarkupString _content;

    [GeneratedRegex(@"\r\n\s*")]
    private static partial Regex DetectIndentsRegex();
}