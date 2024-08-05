using System;
using Entombed.Game.Levels.Serialization;

namespace Entombed;

public static class Tests
{
    public static void LoadLevelXml()
    {
        const string path = "I:\\My_Temp\\test_level_stuff\\example.xml";
        var levelDeserializer = new LevelDeserializer();
        
        var level = levelDeserializer.Deserialize(path);
        
        Console.WriteLine(level);
    }
}