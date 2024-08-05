using System;
using Entombed.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shared.Content;
using Shared.Lifetime;
using Shared.Scenes;
using Keyboard = Shared.Input.Keyboard.Keyboard;

namespace Entombed.MainMenu;

public class MainMenuService(GraphicsDevice graphicsDevice, ContentLookup<SpriteFont> spriteFontLookup, GameConfiguration gameConfiguration, Keyboard keyboard, SceneManager sceneManager) : IInputService, IDrawService
{
    private readonly SpriteBatch _spriteBatch = new(graphicsDevice);
    private readonly State _state = new();
    
    private const string Title = "Entombed";
    private const float TitleScale = 2f;

    private readonly MainMenuOption[] _options =
    [
        new MainMenuOption { Text = "New Game", Action = sceneManager.Transition<GameScene> },
        new MainMenuOption { Text = "Exit", Action = () => { gameConfiguration.Exit = true; } }
    ];

    public void Input()
    {
        var up = keyboard.Get(Keys.Up);
        var down = keyboard.Get(Keys.Down);
        var enter = keyboard.Get(Keys.Enter);
        
        if (down.Pressed)
        {
            _state.SelectedOption = (_state.SelectedOption + 1) % _options.Length;
        }
        
        if (up.Pressed)
        {
            _state.SelectedOption = (_state.SelectedOption + _options.Length - 1) % _options.Length;
        }
        
        if (enter.Pressed)
        {
            _options[_state.SelectedOption].Action();
        }
    }
    
    public void Draw()
    {
        var font = spriteFontLookup.Get(Ids.Arial);
        
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        
        var titleSize = font.MeasureString(Title) * TitleScale;
        var titlePosition = new Vector2((graphicsDevice.Viewport.Width - titleSize.X) / 2, 100);
        _spriteBatch.DrawString(font, Title, titlePosition, Color.White, 0, Vector2.Zero, Vector2.One * TitleScale, SpriteEffects.None, 0);
        
        var screenBottom = graphicsDevice.Viewport.Height;
        
        for (var i = 0; i < _options.Length; i++)
        {
            var option = _options[i];
            var optionSize = font.MeasureString(option.Text);
            var optionPosition = new Vector2((graphicsDevice.Viewport.Width - optionSize.X) / 2, screenBottom - 100 - (_options.Length - i) * 50);
            
            if (i == _state.SelectedOption)
            {
                _spriteBatch.DrawString(font, option.Text, optionPosition, Color.Yellow);
            }
            else
            {
                _spriteBatch.DrawString(font, option.Text, optionPosition, Color.White);
            }
        }
        
        _spriteBatch.End();
    }

    private class State
    {
        public int SelectedOption { get; set; }
    }
    
    private class MainMenuOption
    {
        public required string Text { get; init; }
        public required Action Action { get; init; }
    }
}