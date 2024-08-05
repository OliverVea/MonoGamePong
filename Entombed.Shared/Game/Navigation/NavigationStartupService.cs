using System.Linq;
using Entombed.Game.Levels;
using Shared.Lifetime;
using Shared.Navigation;

namespace Entombed.Game.Navigation;

public class NavigationStartupService(
    RoomLookup roomLookup,
    NavigationGraphService navigationGraphService,
    NavigationState navigationState) : IStartupService
{
    private const float NavigationGraphRadius = 0.75f;

    public void Startup()
    {
        var walls = roomLookup.Values.SelectMany(x => x.Walls).ToArray();
        var areas = roomLookup.Values.SelectMany(x => x.Areas).ToArray();
        
        navigationState.NavigationGraph.Value = navigationGraphService.BuildNavigationGraph(walls, areas, NavigationGraphRadius);
    }
}