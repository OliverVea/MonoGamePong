﻿namespace Shared.Lifetime;

/// <summary>
/// Represents a service that is called on the GUI drawing phase of the game loop.
/// </summary>
public interface IGuiService
{
    /// <summary>
    /// Called on the GUI drawing phase of the game loop.
    /// </summary>
    void DrawGui();
}