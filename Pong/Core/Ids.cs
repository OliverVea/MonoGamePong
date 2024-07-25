using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Core;

public class Ids
{
    public static readonly Id<Texture2D> Paddle = Id<Texture2D>.NewId();
    public static readonly Id<Texture2D> Ball = Id<Texture2D>.NewId();
    public static readonly Id<SoundEffect> Pong = Id<SoundEffect>.NewId();
    public static readonly Id<SpriteFont> Arial = Id<SpriteFont>.NewId();
    public static readonly Id<Effect> FontEffect = Id<Effect>.NewId();
}