namespace Entombed.Game.Menu;

public class GamePaused(MenuState menuState)
{
    public bool Pause { get; set; }
    public bool Paused => Pause || menuState.Show;
}