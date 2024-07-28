using System.Diagnostics.CodeAnalysis;

namespace Shared.Content;

public abstract class Lookup<T>
{
    protected Dictionary<Id<T>, T> Dictionary { get; } = new();
    
    public IReadOnlyCollection<T> Values => Dictionary.Values;
    
    public bool TryGet(Id<T> contentId, [MaybeNullWhen(false)] out T content)
        {
            return Dictionary.TryGetValue(contentId, out content);
        }
        
        public OneOf<T, NotFound> TryGet(Id<T> contentId)
        {
            if (Dictionary.TryGetValue(contentId, out var content)) return content;
            
            return new NotFound();
        }
    
        public T Get(Id<T> contentId)
        {
            if (Dictionary.TryGetValue(contentId, out var content)) return content;
            
            throw new KeyNotFoundException("Could not find content with id");
        }
    
}