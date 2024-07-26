using Microsoft.Xna.Framework.Input;
using Shared.Lifetime;
using Keyboard = Shared.Input.Keyboard.Keyboard;

namespace Entombed.Code.Game.Gui;

public class GuiInputService(GuiState guiState, Keyboard keyboard) : IInputService
{
    private const Keys TriggerNavigationGraphKey = Keys.N;
    private const Keys TriggerMetricsKey = Keys.M;

    public void Input()
    {
        if (keyboard.Get(TriggerNavigationGraphKey).Pressed) guiState.ShowNavigationGraph = !guiState.ShowNavigationGraph;
        if (keyboard.Get(TriggerMetricsKey).Pressed) guiState.ShowMetrics = !guiState.ShowMetrics;
    }
}