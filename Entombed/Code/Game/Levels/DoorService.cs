using Entombed.Code.Game.Characters.Enemies;

namespace Entombed.Code.Game.Levels;

public class DoorService(Level level, EnemySpawningService enemySpawningService)
{
    public void OpenDoor(Door door)
    {
        if (door.Open) return;
        
        door.Open = true;
        door.From.Revealed = true;
        door.To.Revealed = true;
        
        foreach (var room in level.Rooms)
        {
            if (!room.Revealed) continue;
            
            enemySpawningService.SpawnEnemiesForRoom(room);
        }
    }
}