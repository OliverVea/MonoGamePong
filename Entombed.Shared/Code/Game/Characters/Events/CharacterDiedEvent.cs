using System.Diagnostics.CodeAnalysis;

namespace Entombed.Code.Game.Characters.Events;

[method: SetsRequiredMembers]
public class CharacterDiedEvent(Id<Character> targetId, Id<Character>? causeId = null)
{
    public required Id<Character> TargetId { get; init; } = targetId;
    public required Id<Character>? CauseId { get; init; } = causeId;
}