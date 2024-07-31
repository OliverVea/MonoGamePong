using System.Diagnostics.CodeAnalysis;

namespace Entombed.Code.Game.Characters.Events;

public class CharacterDamagedEvent
{
    [SetsRequiredMembers]
    public CharacterDamagedEvent(Id<Character> targetId, float damage)
    {
        AttackerId = null;
        TargetId = targetId;
        Damage = damage;
    }
    
    [SetsRequiredMembers]
    public CharacterDamagedEvent(Id<Character> attackerId, Id<Character> targetId, float damage)
    {
        AttackerId = attackerId;
        TargetId = targetId;
        Damage = damage;
    }
    
    public required Id<Character>? AttackerId { get; init; }
    public required Id<Character> TargetId { get; init; }
    public required float Damage { get; init; }
}