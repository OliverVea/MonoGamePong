using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using Microsoft.Xna.Framework;
using Shared.Geometry.Definitions;
using Shared.Geometry.Shapes;
using Rectangle = Shared.Geometry.Shapes.Rectangle;

namespace Entombed.Game.Levels.Serialization;

public class LevelDeserializer
{
    public Level Deserialize(string path)
    {
        var serializedLevel = DeserializeInternal(path);
        
        return ConvertToLevel(serializedLevel);
    }

    private Level ConvertToLevel(SerializedLevel serializedLevel)
    {
        var rooms = new List<Room>();
        foreach (var serializedRoom in serializedLevel.Rooms)
        {
            var walls = new List<LineSegment>();
            foreach (var serializedWall in serializedRoom.Walls)
            {
                walls.Add(new LineSegment(
                    ConvertToVector2(serializedWall.Start),
                    ConvertToVector2(serializedWall.End)));
            }
            
            var areas = new List<ShapeInput>();
            foreach (var serializedArea in serializedRoom.Areas)
            {
                var center = new Vector2(
                    (serializedArea.TopLeft.X + serializedArea.BottomRight.X) / 2,
                    (serializedArea.TopLeft.Y + serializedArea.BottomRight.Y) / 2);
                
                var size = new Vector2(
                    Math.Abs(serializedArea.TopLeft.X - serializedArea.BottomRight.X),
                    Math.Abs(serializedArea.TopLeft.Y - serializedArea.BottomRight.Y));
                
                areas.Add(new Rectangle(center, size));
            }

            var room = new Room
            {
                Walls = walls.ToArray(),
                Areas = areas.ToArray(),
                Origin = areas.First().Center()
            };
            
            rooms.Add(room);
        }
        
        var doors = new List<Door>();
        foreach (var serializedDoor in serializedLevel.Doors)
        {
            var door = new Door
            {
                From = rooms[serializedDoor.From].Id,
                To = rooms[serializedDoor.To].Id,
                Line = new LineSegment(
                    ConvertToVector2(serializedDoor.Start),
                    ConvertToVector2(serializedDoor.End))
            };
            doors.Add(door);
        }
        
        var goal = serializedLevel.Goal != null ? ConvertToVector2(serializedLevel.Goal) : Vector2.Zero;
        var stairs = serializedLevel.Stairs != null ? ConvertToVector2(serializedLevel.Stairs) : Vector2.Zero;

        return new Level
        {
            Rooms = rooms,
            Doors = doors,
            Goal = goal,
            Stairs = stairs
        };
    }
    
    private Vector2 ConvertToVector2(SerializedPoint point)
    {
        return new Vector2(point.X, point.Y);
    }

    private SerializedLevel DeserializeInternal(string path)
    {
        var xmlText = System.IO.File.ReadAllText(path);
        var serializedLevel = ParseXml(xmlText);
        
        return serializedLevel;
    }

    private static SerializedLevel ParseXml(string xmlText)
    {
        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlText);
        
        var levelNode = xmlDoc.SelectSingleNode("Level");
        if (levelNode == null)
        {
            throw new Exception("Invalid XML: Missing <Level> root element.");
        }
        
        // Parse Rooms
        var roomsNode = levelNode.SelectSingleNode("Rooms");
        var rooms = ParseRooms(roomsNode);
        
        // Parse Doors
        var doorsNode = levelNode.SelectSingleNode("Doors");
        var doors = ParseDoors(doorsNode);
        
        // Parse Goal
        var goalNode = levelNode.SelectSingleNode("Goal");
        var goal = goalNode != null ? ParsePoint(goalNode.InnerText) : null;
        
        // Parse Stairs
        var stairsNode = levelNode.SelectSingleNode("Stairs");
        var stairs = stairsNode != null ? ParsePoint(stairsNode.InnerText) : null;

        return new SerializedLevel
        {
            Rooms = rooms,
            Doors = doors,
            Goal = goal,
            Stairs = stairs
        };
    }

    private static IReadOnlyCollection<SerializedDoor> ParseDoors(XmlNode? doorsNode)
    {
        if (doorsNode == null) return new List<SerializedDoor>();

        var doorNodes = doorsNode.SelectNodes("Door");
        if (doorNodes == null) return new List<SerializedDoor>();
        
        var doors = new List<SerializedDoor>();
        foreach (XmlNode doorNode in doorNodes)
        {
            var lineNode = doorNode.SelectSingleNode("Line");
            if (lineNode == null) continue;
            
            var points = ParsePoints(lineNode.InnerText);
            
            var fromNode = doorNode.SelectSingleNode("From");
            var toNode = doorNode.SelectSingleNode("To");
            
            if (fromNode == null || toNode == null) continue;
            
            doors.Add(new SerializedDoor
            {
                Start = points.Item1,
                End = points.Item2,
                From = int.Parse(fromNode.InnerText),
                To = int.Parse(toNode.InnerText)
            });
        }
        
        return doors;
    }

    private static IReadOnlyCollection<SerializedRoom> ParseRooms(XmlNode? roomsNode)
    {
        if (roomsNode == null) return new List<SerializedRoom>();

        var roomNodes = roomsNode.SelectNodes("Room");
        if (roomNodes == null) return new List<SerializedRoom>();
        
        var rooms = new List<SerializedRoom>();
        foreach (XmlNode roomNode in roomNodes)
        {
            var walls = GetWalls(roomNode);
            var areas = GetAreas(roomNode);
            rooms.Add(new SerializedRoom { Walls = walls, Areas = areas });
        }
        
        return rooms;
    }
    
    private static IReadOnlyCollection<SerializedWall> GetWalls(XmlNode roomNode)
    {
        var wallsNode = roomNode.SelectSingleNode("Walls");
        if (wallsNode == null) return new List<SerializedWall>();
        
        var wallNodes = wallsNode.SelectNodes("Wall");
        if (wallNodes == null) return new List<SerializedWall>();
        
        var walls = new List<SerializedWall>();
        foreach (XmlNode wallNode in wallNodes)
        {
            var points = ParsePoints(wallNode.InnerText);
            walls.Add(new SerializedWall { Start = points.Item1, End = points.Item2 });
        }
        
        return walls;
    }
    
    private static IReadOnlyCollection<SerializedArea> GetAreas(XmlNode roomNode)
    {
        var areasNode = roomNode.SelectSingleNode("Areas");
        if (areasNode == null) return new List<SerializedArea>();
        
        var rectNodes = areasNode.SelectNodes("Rectangle");
        if (rectNodes == null) return new List<SerializedArea>();
        
        var areas = new List<SerializedArea>();
        foreach (XmlNode rectNode in rectNodes)
        {
            var points = ParsePoints(rectNode.InnerText);
            areas.Add(new SerializedArea { TopLeft = points.Item1, BottomRight = points.Item2 });
        }
        
        return areas;
    }

    private static Tuple<SerializedPoint, SerializedPoint> ParsePoints(string pointText)
    {
        var points = pointText.Split(' ');
        var start = ParsePoint(points[0]);
        var end = ParsePoint(points[1]);
        return new Tuple<SerializedPoint, SerializedPoint>(start, end);
    }

    private static SerializedPoint ParsePoint(string pointText)
    {
        var coords = pointText.Trim('(', ')').Split(',');
        
        var point = new SerializedPoint
        {
            X = float.Parse(coords[0], CultureInfo.InvariantCulture),
            Y = float.Parse(coords[1], CultureInfo.InvariantCulture)
        };
        
        return point;
    }
}

public class SerializedLevel
{
    public required IReadOnlyCollection<SerializedRoom> Rooms { get; init; }
    public required IReadOnlyCollection<SerializedDoor> Doors { get; init; }
    public SerializedPoint? Goal { get; init; }
    public SerializedPoint? Stairs { get; init; }
}

public class SerializedRoom
{
    public required IReadOnlyCollection<SerializedWall> Walls { get; init; }
    public required IReadOnlyCollection<SerializedArea> Areas { get; init; }
}

public class SerializedWall
{
    public required SerializedPoint Start { get; init; }
    public required SerializedPoint End { get; init; }
}

public class SerializedArea
{
    public required SerializedPoint TopLeft { get; init; }
    public required SerializedPoint BottomRight { get; init; }
}

public class SerializedDoor
{
    public required SerializedPoint Start { get; init; }
    public required SerializedPoint End { get; init; }
    public required int From { get; init; }
    public required int To { get; init; }
}

public class SerializedPoint
{
    public float X { get; init; }
    public float Y { get; init; }
}