namespace Shared.Lifetime;

/// <summary>
/// Represents a service that is called on the input phase of the game loop.
/// </summary>
public interface IInputService
{
    /// <summary>
    /// Called on the input phase of the game loop.
    /// </summary>
    void Input();
}