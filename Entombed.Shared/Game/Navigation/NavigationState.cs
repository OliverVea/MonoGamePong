using System.Collections.Generic;
using Entombed.Game.Levels;
using Shared.Navigation;

namespace Entombed.Game.Navigation;

public class NavigationState
{
    public required Dictionary<(Id<Room> From, Id<Room> To), NavigationPath> RoomPaths { get; init; }
    public required NavigationGraph NavigationGraph { get; init; }
}