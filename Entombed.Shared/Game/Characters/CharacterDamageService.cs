using System;
using Entombed.Game.Characters.Events;
using Microsoft.Extensions.Logging;

namespace Entombed.Game.Characters;

public class CharacterDamageService(
    ILogger<CharacterDamageService> logger,
    CharacterLookup characterLookup,
    IEventInvoker<CharacterDiedEvent> characterDiedEvent,
    IEventInvoker<CharacterDamagedEvent> characterDamagedEvent)
{
    public void Damage(Id<Character> attackerId, Id<Character> targetId, float damage)
    {
        if (attackerId == targetId || damage == 0f) return;
        
        if (!characterLookup.TryGet(targetId, out var target)) return;
        
        target.Health -= damage;
        target.AttackedTime = DateTime.Now;
        
        logger.LogInformation($"{attackerId} damaged {target} for {damage}");

        if (target.Health < 0) target.Health = 0;
        
        characterDamagedEvent.Invoke(new CharacterDamagedEvent(attackerId, targetId, damage));
        
        if (target.Health == 0) Kill(attackerId, targetId);
    }
    
    public void Kill(Id<Character> targetId)
    {
        if (!characterLookup.TryGet(targetId, out var target))
        {
            logger.LogWarning($"Failed to kill {targetId}");
            return;
        }
        
        target.Health = 0;
        
        logger.LogInformation($"{targetId} was killed");
        
        characterDiedEvent.Invoke(new CharacterDiedEvent(targetId));
    }
    
    public void Kill(Id<Character> causeId, Id<Character> targetId)
    {
        if (!characterLookup.TryGet(targetId, out var target))
        {
            logger.LogWarning($"Failed to kill {targetId}");
            return;
        }
        
        target.Health = 0;
        
        logger.LogInformation($"{causeId} killed {targetId}");
        
        characterDiedEvent.Invoke(new CharacterDiedEvent(targetId, causeId));
    }
}