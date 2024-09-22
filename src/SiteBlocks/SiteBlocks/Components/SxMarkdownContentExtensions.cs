using System;
using System.IO;
using Microsoft.AspNetCore.Components;

namespace Stellaxis.SiteBlocks.Components;

public static class SxMarkdownContentExtensions
{
    public static string GetFromSourceCodePath<T>(this T component, string filePath) 
        where T : ComponentBase
    {
        var processFullPath = Environment.ProcessPath;
        ArgumentException.ThrowIfNullOrEmpty(processFullPath, nameof(processFullPath));
        
        var processFileInfo = new FileInfo(processFullPath);
        var processDirectoryPath = processFileInfo.Directory?.FullName;
        ArgumentException.ThrowIfNullOrEmpty(processDirectoryPath, nameof(processDirectoryPath));
        
        var componentType = typeof(T);
        var componentNamespace = componentType.Namespace;
        ArgumentException.ThrowIfNullOrEmpty(componentNamespace, nameof(componentNamespace));
        
        var assemblyNamespace = componentType.Assembly.GetName().Name;
        ArgumentException.ThrowIfNullOrEmpty(assemblyNamespace, nameof(assemblyNamespace));
        
        var deltaNamespace = componentNamespace.Substring(assemblyNamespace.Length, componentNamespace.Length - assemblyNamespace.Length);
        var deltaPath = deltaNamespace.Replace('.', Path.DirectorySeparatorChar).TrimStart(Path.DirectorySeparatorChar);
        
        var result = Path.Combine(processDirectoryPath, deltaPath, filePath);

        return result;
    }
}