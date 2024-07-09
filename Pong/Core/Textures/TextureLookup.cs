using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Core.Textures;

public class TextureLookup(IEnumerable<ITextureLoader> textureLoaders, GraphicsDevice graphicsDevice) : Initializable
{
    private readonly Dictionary<Id<Texture2D>, Texture2D> _lookup = new();
    
    public override void Initialize()
    {
        foreach (var textureLoader in textureLoaders)
        {
            var (textureId, texture) = textureLoader.LoadTexture(graphicsDevice);

            if (_lookup.TryAdd(textureId, texture)) continue;
            throw new ApplicationException("Tried to register texture with same id twice.");
        }
    }

    public Texture2D GetTexture(Id<Texture2D> textureId)
    {
        if (_lookup.TryGetValue(textureId, out var texture)) return texture;

        throw new KeyNotFoundException("Could not find texture with the provided id.");
    }
}