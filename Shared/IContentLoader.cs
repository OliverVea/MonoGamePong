namespace Shared;

public interface IContentLoader<T>
{
    (Id<T> ContentId, T Content) Load();
}