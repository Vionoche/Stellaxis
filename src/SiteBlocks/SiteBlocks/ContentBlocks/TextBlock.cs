using System;
using FluentValidation;
using Stellaxis.SiteBlocks.Pages;

namespace Stellaxis.SiteBlocks.ContentBlocks;

public sealed record TextBlock(
    Guid ContentBlockId,
    ContentBlockComponentType ContentBlockComponentType,
    string Text)
    : ContentBlock(ContentBlockId, ContentBlockComponentType)
{
    public static TextBlock Create(
        Guid contentBlockId,
        ContentBlockComponentType contentBlockComponentType,
        string text)
    {
        var rule = new TextBlockRule(text);
        new TextBlockRuleValidator().ValidateAndThrow(rule);
        
        return new TextBlock(contentBlockId, contentBlockComponentType, text);
    }
}