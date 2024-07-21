using StrictId;

namespace Shared.Content;

public interface IContentLoader<T>
{
    (Id<T> ContentId, T Content) Load();
}