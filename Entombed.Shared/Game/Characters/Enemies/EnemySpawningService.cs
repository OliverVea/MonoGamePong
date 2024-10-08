using System;
using System.Linq;
using Entombed.Game.Characters.Events;
using Entombed.Game.Levels;
using Microsoft.Xna.Framework;
using Shared.Lifetime;

namespace Entombed.Game.Characters.Enemies;

public class EnemySpawningService(
    IEventInvoker<CharacterSpawnedEvent> characterSpawnedEvent,
    IEventObserver<DoorOpenedEvent> doorOpenedEvent,
    RoomLookup roomLookup) : IStartupService
{

    public void Startup()
    {
        doorOpenedEvent.Subscribe(OnDoorOpened);
    }
    
    private void OnDoorOpened(DoorOpenedEvent e)
    {
        var unlitRooms = roomLookup.Values.Where(x => x is { Revealed: true, Lit: false });
        
        foreach (var room in unlitRooms) SpawnEnemiesForRoom(room);
    }

    private void SpawnEnemiesForRoom(Room room)
    {
        var random = new Random();
        var enemyCount = random.Next(1, 4);

        var roomCorners = room.Walls.SelectMany(x => new[] { x.End, x.Start }).Distinct().ToList();
        
        var top = roomCorners.Max(x => x.Y) - 0.2f;
        var bottom = roomCorners.Min(x => x.Y) + 0.2f;
        var height = top - bottom;
        var left = roomCorners.Min(x => x.X) + 0.2f;
        var right = roomCorners.Max(x => x.X) - 0.2f;
        var width = right - left;
        
        for (var i = 0; i < enemyCount; i++)
        {
            var position = new Vector2(left + (float)random.NextDouble() * width, bottom + (float)random.NextDouble() * height);
            SpawnEnemy(position);
        }
    }

    private void SpawnEnemy(Vector2 position)
    {
        var enemy = new Enemy
        {
            Position = position
        };
        
        characterSpawnedEvent.Invoke(new CharacterSpawnedEvent(enemy));
    }
}