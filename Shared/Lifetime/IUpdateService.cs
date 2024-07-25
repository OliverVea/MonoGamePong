namespace Shared.Lifetime;

/// <summary>
/// Represents a service that is called on the update phase of the game loop.
/// </summary>
public interface IUpdateService
{
    /// <summary>
    /// Called on the update phase of the game loop.
    /// </summary>
    void Update();
}