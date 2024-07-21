using StrictId;

namespace Shared.Content;

public interface IContentLoader<T>
{
    IEnumerable<(Id<T> ContentId, T Content)> Load();
}