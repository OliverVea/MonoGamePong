namespace Shared.Lifetime;

public interface IUpdateService
{
    /// <summary>
    /// Called on the update phase of the game loop.
    /// </summary>
    void Update();
}