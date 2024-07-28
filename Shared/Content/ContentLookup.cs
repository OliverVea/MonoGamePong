using System.Diagnostics.CodeAnalysis;
using Shared.Lifetime;

namespace Shared.Content;

public class ContentLookup<T>(IEnumerable<IContentLoader<T>> contentLoaders) : Lookup<T>, IStartupService
{
    public void Startup()
    {
        foreach (var contentLoader in contentLoaders)
        {
            LoadContent(contentLoader);
        }
    }
    
    private void LoadContent(IContentLoader<T> contentLoader)
    {
        foreach (var (contentId, content) in contentLoader.Load())
        {
            if (Dictionary.TryAdd(contentId, content)) continue;
            throw new ApplicationException("Tried to register content with same id twice");
        }
    }

    
}