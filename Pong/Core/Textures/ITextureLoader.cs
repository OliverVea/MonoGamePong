using Microsoft.Xna.Framework.Graphics;

namespace Pong.Core.Textures;

public interface ITextureLoader
{
    (Id<Texture2D> TextureId, Texture2D Texture) LoadTexture(GraphicsDevice graphicsDevice);
}