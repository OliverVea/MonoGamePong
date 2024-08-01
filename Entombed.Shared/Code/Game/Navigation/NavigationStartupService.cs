using System.Linq;
using Entombed.Code.Game.Levels;
using Shared.Lifetime;
using Shared.Navigation;

namespace Entombed.Code.Game.Navigation;

public class NavigationStartupService(
    RoomLookup roomLookup,
    NavigationGraphService navigationGraphService,
    NavigationState navigationState) : IStartupService
{
    private const float NavigationGraphRadius = 0.75f;

    public void Startup()
    {
        var walls = roomLookup.Values.SelectMany(x => x.Walls).ToArray();
        
        navigationState.NavigationGraph.Value = navigationGraphService.BuildNavigationGraph(walls, NavigationGraphRadius);
    }
}