using Entombed.MainMenu;
using Shared.Lifetime;
using Shared.Scenes;

namespace Entombed.Game.Menu;

public class MenuUpdateService(
    MenuInput menuInput,
    MenuState menuState,
    GameConfiguration gameConfiguration,
    SceneManager sceneManager) : IUpdateService
{
    public void Update()
    {
        if (menuInput.ToggleShow) menuState.Show = !menuState.Show;

        if (!menuState.Show) return;

        if (menuInput.Up) DecrementOption();
        if (menuInput.Down) IncrementOption();
        
        if (menuInput.Select) SelectOption();
    }

    private void IncrementOption() => menuState.SelectedOption = menuState.SelectedOption == (MenuState.MenuOption)MenuState.MenuOptions.Count - 1 ? 
        0 : 
        menuState.SelectedOption + 1;

    private void DecrementOption() => menuState.SelectedOption = menuState.SelectedOption == 0 ? 
        (MenuState.MenuOption)MenuState.MenuOptions.Count - 1 :
        menuState.SelectedOption - 1;
    
    private void SelectOption()
    {
        switch (menuState.SelectedOption)
        {
            case MenuState.MenuOption.Resume:
                menuState.Show = false;
                break;
            case MenuState.MenuOption.MainMenu:
                sceneManager.Transition<MainMenuScene>();
                break;
            case MenuState.MenuOption.Exit:
                gameConfiguration.Exit = true;
                break;
        }
    }
}