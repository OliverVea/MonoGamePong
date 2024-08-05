using System.Diagnostics.CodeAnalysis;

namespace Entombed.Game.Characters.Events;

[method: SetsRequiredMembers]
public class CharacterSpawnedEvent(Character character)
{
    public required Character Character { get; set; } = character;
}