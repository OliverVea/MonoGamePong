using System.Diagnostics.CodeAnalysis;
using Shared.Lifetime;
using StrictId;

namespace Shared.Content;

public class ContentLookup<T>(IEnumerable<IContentLoader<T>> loaders) : IStartupService
{
    private readonly Dictionary<Id<T>, T> _lookup = new();
    
    public void Startup()
    {
        foreach (var textureLoader in loaders)
        {
            var (textureId, texture) = textureLoader.Load();

            if (_lookup.TryAdd(textureId, texture)) continue;
            throw new ApplicationException("Tried to register texture with same id twice");
        }
    }

    public bool TryGet(Id<T> contentId, [MaybeNullWhen(false)] out T content)
    {
        return _lookup.TryGetValue(contentId, out content);
    }

    public T Get(Id<T> contentId)
    {
        if (_lookup.TryGetValue(contentId, out var content)) return content;
        
        throw new KeyNotFoundException("Could not find content with id");
    }
}