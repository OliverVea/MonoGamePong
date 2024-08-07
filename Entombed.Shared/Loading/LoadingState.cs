using System.Threading.Tasks;
using Entombed.Game;

namespace Entombed.Loading;

public class LoadingState
{
    public RequireInitialization<Task> LoadingTask { get; } = new();
    public string LoadingText { get; set; } = string.Empty;
    public GameScene? GameScene { get; set; }
}