namespace FluidSimulation.Game;

public class Particles
{
    public required ParticleData[] Data { get; set; }
    public int Count => Data.Length;
}

public readonly struct ParticleData
{
    public float Radius { get; init; }
    public float X { get; init; }
    public float Y { get; init; }
    public float VelocityX { get; init; }
    public float VelocityY { get; init; }
    public float AccelerationX { get; init; }
    public float AccelerationY { get; init; }
}