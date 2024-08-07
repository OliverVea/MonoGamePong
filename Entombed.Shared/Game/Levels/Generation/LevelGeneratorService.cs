using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Shared.Geometry.Shapes;
using Shared.Random;
using Rectangle = Shared.Geometry.Shapes.Rectangle;

namespace Entombed.Game.Levels.Generation;

public class LevelGeneratorService(ILogger<LevelGeneratorService> logger)
{
    private const int RoomCountMin = 10;
    private const int RoomCountMax = 15;
    
    private const int RoomWidthMin = 5;
    private const int RoomWidthMax = 10;
    
    private const int RoomHeightMin = 5;
    private const int RoomHeightMax = 10;
    
    private const float DoorWidth = 1;
    
    private static readonly Vector2[] Directions =
    [
        new Vector2(1, 0),
        new Vector2(-1, 0),
        new Vector2(0, 1),
        new Vector2(0, -1)
    ];
    
    public Level GenerateLevel()
    {
        var roomCount = RandomHelper.RandomInt(RoomCountMin, RoomCountMax);
        
        Span<Rectangle> roomRectangles = stackalloc Rectangle[roomCount];
        Span<DoorData> doorDatas = stackalloc DoorData[roomCount - 1];
        
        roomRectangles[0] = GenerateRoom();
        
        for (var i = 1; i < roomCount; i++)
        {
            var addedRoom = false;

            do
            {
                var originRoomIndex = RandomHelper.RandomInt(0, i - 1);
                var originRoom = roomRectangles[originRoomIndex];
                var newRoom = GenerateRoom();
                
                var direction = RandomHelper.RandomElement(Directions);
                var newRoomPosition = originRoom.Center + direction * (originRoom.Size + newRoom.Size) / 2;
                
                newRoom = new Rectangle(newRoomPosition, newRoom.Size);
                
                var intersects = false;
                
                foreach (var room in roomRectangles[..i])
                {
                    if (room.Overlaps(newRoom))
                    {
                        intersects = true;
                        break;
                    }
                }
                
                if (!intersects)
                {
                    roomRectangles[i] = newRoom;
                    addedRoom = true;
                    
                    var doorPosition = originRoom.Center + direction * originRoom.Size / 2;
                    
                    var doorData = new DoorData
                    {
                        Position = doorPosition,
                        OriginRoomIndex = originRoomIndex,
                        Vertical = direction.X != 0,
                    };
                    
                    doorDatas[i - 1] = doorData;
                }
                
            } while (!addedRoom);
        }

        var rooms = GetRooms(roomRectangles, doorDatas);
        var doors = GetDoors(rooms, doorDatas);

        var stairsRoom = RandomHelper.RandomElement(rooms[1..]);
        
        return new Level
        {
            Rooms = rooms,
            Doors = doors,
            Stairs = stairsRoom.Origin,
            Goal = rooms[0].Origin
        };
    }

    private Room[] GetRooms(in ReadOnlySpan<Rectangle> roomRectangles, in ReadOnlySpan<DoorData> doorData)
    {
        var rooms = new Room[roomRectangles.Length];
        
        for (var i = 0; i < roomRectangles.Length; i++)
        {
            List<LineSegment> walls =
            [
                new LineSegment(roomRectangles[i].TopLeft, roomRectangles[i].TopRight),
                new LineSegment(roomRectangles[i].TopRight, roomRectangles[i].BottomRight),
                new LineSegment(roomRectangles[i].BottomRight, roomRectangles[i].BottomLeft),
                new LineSegment(roomRectangles[i].BottomLeft, roomRectangles[i].TopLeft)
            ];

            foreach (var door in doorData)
            {
                for (var k = 0; k < walls.Count; k++)
                {
                    var wall = walls[k];
                    if (!wall.Intersects(door.Position)) continue;
                    
                    var split = SplitAroundDoor(wall, door);
                    
                    logger.LogInformation("Splitting wall {Wall} around door {Door}", wall, door);
                    
                    walls.RemoveAt(k);
                    walls.InsertRange(k, split);
                }
            }
            
            rooms[i] = new Room
            {
                Areas = [ roomRectangles[i] ],
                Revealed = i == 0,
                Lit = i == 0,
                Walls = walls.ToArray(),
                Origin = roomRectangles[i].Center
            };
        }
        
        return rooms;
    }

    private Door[] GetDoors(IReadOnlyList<Room> rooms, in ReadOnlySpan<DoorData> doorData)
    {
        var doors = new Door[doorData.Length];

        for (var i = 0; i < doorData.Length; i++)
        {
            var from = rooms[doorData[i].OriginRoomIndex].Id;
            var to = rooms[i + 1].Id;

            var delta = doorData[i].Vertical ? new Vector2(0, 1) : new Vector2(1, 0);
            delta *= DoorWidth;
            
            var line = new LineSegment(doorData[i].Position - delta / 2f, doorData[i].Position + delta / 2f);

            doors[i] = new Door
            {
                From = from,
                To = to,
                Line = line
            };
        }

        return doors;
    }

    private static Rectangle GenerateRoom()
    {
        var width = RandomHelper.RandomInt(RoomWidthMin, RoomWidthMax);
        var height = RandomHelper.RandomInt(RoomHeightMin, RoomHeightMax);
        
        return new Rectangle(Vector2.Zero, new Vector2(width, height));
    }

    private readonly struct DoorData
    {
        public Vector2 Position { get; init; }
        public int OriginRoomIndex { get; init; }
        public bool Vertical { get; init; }
    }

    private static LineSegment[] SplitAroundDoor(LineSegment wall, DoorData doorData)
    {
        var result = new LineSegment[2];
        var direction = doorData.Vertical ? Vector2.UnitY : Vector2.UnitX;
        
        var doorStart = doorData.Position - direction * DoorWidth / 2;
        var doorEnd = doorData.Position + direction * DoorWidth / 2;
        
        var start = wall.Start;
        var end = wall.End;
        
        if (doorData.Vertical)
        {
            if (start.Y < doorStart.Y)
            {
                result[0] = new LineSegment(start, doorStart);
                result[1] = new LineSegment(doorEnd, end);
            }
            else
            {
                result[0] = new LineSegment(start, doorEnd);
                result[1] = new LineSegment(doorStart, end);
            }
        }
        else
        {
            if (start.X < doorStart.X)
            {
                result[0] = new LineSegment(start, doorStart);
                result[1] = new LineSegment(doorEnd, end);
            }
            else
            {
                result[0] = new LineSegment(start, doorEnd);
                result[1] = new LineSegment(doorStart, end);
            }
        }
        
        return result;
    }
}