using System.Diagnostics.CodeAnalysis;

namespace Shared;

public class ContentLookup<T>(IEnumerable<IContentLoader<T>> loaders) : Initializable
{
    private readonly Dictionary<Id<T>, T> _lookup = new();
    
    public override void Initialize()
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