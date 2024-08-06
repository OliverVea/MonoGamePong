using System;
using System.Collections.Generic;

namespace Entombed.Game.Menu;

public class MenuState
{
    public bool Show { get; set; }
    public MenuOption SelectedOption { get; set; } = 0;
    public const int MenuItems = 6;
    
    public static IReadOnlyList<MenuOption> MenuOptions { get; } = Enum.GetValues<MenuOption>();
    
    public enum MenuOption
    {
        Resume,
        //Save,
        //Load,
        //Options,
        MainMenu,
        Exit
    }
    
    public static string GetOptionText(MenuOption option)
    {
        return option switch
        {
            MenuOption.Resume => "Resume",
            //MenuOption.Save => "Save",
            //MenuOption.Load => "Load",
            //MenuOption.Options => "Options",
            MenuOption.MainMenu => "Main Menu",
            MenuOption.Exit => "Exit",
            _ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
        };
    }
}