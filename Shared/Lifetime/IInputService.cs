namespace Shared.Lifetime;

public interface IInputService
{
    /// <summary>
    /// Called on the input phase of the game loop.
    /// </summary>
    void Input();
}