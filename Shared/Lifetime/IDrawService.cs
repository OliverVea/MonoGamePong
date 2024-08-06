namespace Shared.Lifetime;

/// <summary>
/// Represents a service that is called on the game drawing phase of the game loop.
/// </summary>
public interface IDrawService
{
    /// <summary>
    /// Called on the drawing phase of the game loop.
    /// </summary>
    void Draw();

    public int DrawPriority => 0;
    bool Active => true;
}