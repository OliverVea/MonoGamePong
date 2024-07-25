using System.Diagnostics.CodeAnalysis;
using Shared.Lifetime;

namespace Shared.Content;

public class ContentLookup<T>(IEnumerable<IContentLoader<T>> contentLoaders) : IStartupService
{
    private readonly Dictionary<Id<T>, T> _lookup = new();
    
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
            if (_lookup.TryAdd(contentId, content)) continue;
            throw new ApplicationException("Tried to register content with same id twice");
        }
    }

    public bool TryGet(Id<T> contentId, [MaybeNullWhen(false)] out T content)
    {
        return _lookup.TryGetValue(contentId, out content);
    }
    
    public OneOf<T, NotFound> TryGet(Id<T> contentId)
    {
        if (_lookup.TryGetValue(contentId, out var content)) return content;
        
        return new NotFound();
    }

    public T Get(Id<T> contentId)
    {
        if (_lookup.TryGetValue(contentId, out var content)) return content;
        
        throw new KeyNotFoundException("Could not find content with id");
    }
}