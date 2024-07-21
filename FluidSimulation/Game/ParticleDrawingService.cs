using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Shared.Lifetime;
using Shared.Screen;

namespace FluidSimulation.Game;

public class ParticleDrawingService(GraphicsDevice graphicsDevice, ContentManager contentManager, Particles particles, Screen screen) : IDrawService, IStartupService
{
    private const int CircleDiameter = 12;
    
    private static readonly Vector2 CameraCenter = new(0f, 0f);
    private const float CameraZoom = 10f;
    
    private readonly RequireInitialization<Texture2D> _circleTexture = new();

    public void Startup()
    {
        _circleTexture.Value = new Texture2D(graphicsDevice, CircleDiameter, CircleDiameter);
        
        var center = new Vector2(CircleDiameter / 2f, CircleDiameter / 2f);
        var textureData = new Color[CircleDiameter * CircleDiameter];
        
        for (var i = 0; i < textureData.Length; i++)
        {
            var col = i % CircleDiameter;
            var row = i / CircleDiameter;
            
            var pixelPosition = new Vector2(col, row);
            
            var dist = Vector2.Distance(center, pixelPosition);
            textureData[i] = dist < CircleDiameter / 2f ? Color.White : Color.Transparent;
        }
        
        _circleTexture.Value.SetData(textureData);
    }
    
    public void Draw()
    {
        var spriteBatch = new SpriteBatch(graphicsDevice);
        
        spriteBatch.Begin();
        
        foreach (var particle in particles.Data)
        {
            var position = new Vector2(particle.X, particle.Y);
            var radius = particle.Radius;
            
            var screenPosition = CameraCenter + (position - CameraCenter) * CameraZoom;
            var screenRadius = radius * CameraZoom;
            
            spriteBatch.Draw(_circleTexture.Value, screenPosition, null, Color.White, 0, new Vector2(CircleDiameter / 2f), screenRadius / (CircleDiameter / 2f), SpriteEffects.None, 0);
        }
        
        spriteBatch.End();
    }
}