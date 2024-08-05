using Shared.Navigation;

namespace Entombed.Game.Navigation;

public class NavigationState
{
    public RequireInitialization<NavigationGraph> NavigationGraph { get; set; } = new();
}