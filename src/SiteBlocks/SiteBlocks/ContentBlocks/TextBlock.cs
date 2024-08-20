using System;
using ChillSite.SiteBlocks.Pages;
using FluentValidation;

namespace ChillSite.SiteBlocks.ContentBlocks;

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