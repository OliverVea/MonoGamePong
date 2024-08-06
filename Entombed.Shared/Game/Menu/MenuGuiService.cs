using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shared.Content;
using Shared.Lifetime;
using Shared.Screen;

namespace Entombed.Game.Menu;

public class MenuGuiService : IGuiService
{
    private const int Priority = -1000;
    private const float Transparency = 0.7f;
    
    private readonly MenuState _menuState;
    private readonly ContentLookup<SpriteFont> _spriteFontLookup;
    private readonly SpriteBatch _spriteBatch;
    private readonly Texture2D _backgroundTexture;
    
    private static readonly MenuState.MenuOption[] MenuOptions = Enum.GetValues<MenuState.MenuOption>();

    public MenuGuiService(MenuState menuState, GraphicsDevice graphicsDevice, Screen screenData, ContentLookup<SpriteFont> spriteFontLookup)
    {
        _menuState = menuState;
        _spriteFontLookup = spriteFontLookup;
        _spriteBatch = new SpriteBatch(graphicsDevice);
        
        var backgroundTexture = new Texture2D(graphicsDevice, screenData.Width, screenData.Height);
        
        var data = new Color[screenData.Width * screenData.Height];
        for (var i = 0; i < data.Length; ++i) data[i] = Color.Black * Transparency;
        backgroundTexture.SetData(data);
        
        _backgroundTexture = backgroundTexture;
    }
    
    public int GuiPriority => Priority;
    public bool Active => _menuState.Show;

    public void DrawGui()
    {
        var font = _spriteFontLookup.Get(Ids.Arial);
        
        _spriteBatch.Begin();
        
        _spriteBatch.Draw(_backgroundTexture, Vector2.Zero, Color.White);
        
        for (var i = 0; i < MenuOptions.Length; i++)
        {
            var option = MenuOptions[i];
            var text = MenuState.GetOptionText(option);
            var color = i == (int) _menuState.SelectedOption ? Color.White : Color.Gray;
            
            _spriteBatch.DrawString(
                font,
                text,
                new Vector2(100, 100 + i * 50),
                color);
        }
        
        _spriteBatch.End();
    }
}