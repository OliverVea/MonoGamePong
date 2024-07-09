namespace Shared;

public interface IGameDrawerService<in T>
{
    void Draw(T gameInput);
}