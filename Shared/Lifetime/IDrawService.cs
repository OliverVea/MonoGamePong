namespace Shared.Lifetime;

public interface IDrawService
{
    /// <summary>
    /// Called on the drawing phase of the game loop.
    /// </summary>
    void Draw();
}