namespace Shared.Sprites;

public class CharacterSprite
{
    public SpriteAnimation Current => GetSpriteAnimation();
    public required SpriteAnimation IdleFront { get; set; }
    public CharacterSpriteState State { get; set; } = CharacterSpriteState.IdleFront;

    private SpriteAnimation GetSpriteAnimation()
    {
        return State switch
        {
            CharacterSpriteState.IdleFront => IdleFront,
            CharacterSpriteState.None => throw new Exception("CharacterSpriteState None should never be evaluated for SpriteAnimation"),
            _ => throw new Exception($"GetSpriteAnimation does not support CharacterSpriteState {State}")
        };
    }
}