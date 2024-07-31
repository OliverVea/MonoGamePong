using System.Diagnostics.CodeAnalysis;

namespace Entombed.Code.Game.Characters.Events;

[method: SetsRequiredMembers]
public class CharacterSpawnedEvent(Character character)
{
    public required Character Character { get; set; } = character;
}