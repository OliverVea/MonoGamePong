using System.ComponentModel.DataAnnotations;

namespace Shared.Options;

public class DisplayOptions
{
    public const string SectionName = "Display";
    
    [Range(640, 3840)]
    public int Width { get; init; } = 1280;
    
    [Range(480, 2160)]
    public int Height { get; init; } = 720;
    
    public required bool Fullscreen { get; init; } = false;
}