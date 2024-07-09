namespace Shared;

public interface IGameLogicService<in T>
{
    void Update(T gameInput);
}