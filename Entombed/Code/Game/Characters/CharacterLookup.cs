using Entombed.Code.Game.Characters.Players;
using Shared.Content;

namespace Entombed.Code.Game.Characters;

public class CharacterLookup : Lookup<Character>
{
    public CharacterLookup(Player player)
    {
        TryAdd(player);
    }
    
    public bool TryAdd(Character character)
    {
        return Dictionary.TryAdd(character.Id, character);
    }
}