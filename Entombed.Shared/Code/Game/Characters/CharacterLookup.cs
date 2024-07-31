using Entombed.Code.Game.Characters.Events;
using Entombed.Code.Game.Characters.Players;
using Shared.Content;
using Shared.Events;

namespace Entombed.Code.Game.Characters;

public class CharacterLookup : Lookup<Character>
{
    public CharacterLookup(Player player,
        IEventObserver<CharacterSpawnedEvent> characterSpawnedEvent,
        IEventObserver<CharacterDiedEvent> characterDiedEvent)
    {
        TryAdd(player);
        
        characterSpawnedEvent.Subscribe(OnCharacterSpawned);
        characterDiedEvent.Subscribe(OnCharacterDied);
    }

    private void OnCharacterDied(CharacterDiedEvent characterDiedEvent)
    {
        TryRemove(characterDiedEvent.TargetId);
    }

    private void OnCharacterSpawned(CharacterSpawnedEvent characterSpawnedEvent)
    {
        TryAdd(characterSpawnedEvent.Character);
    }

    private bool TryAdd(Character character) => Dictionary.TryAdd(character.Id, character);

    private bool TryRemove(Id<Character> targetId) => Dictionary.Remove(targetId);
}