using Shared.Navigation;

namespace Entombed.Code.Game.Navigation;

public class NavigationState
{
    public RequireInitialization<NavigationGraph> NavigationGraph { get; set; } = new();
}