using System;
using Microsoft.Xna.Framework;
using Shared.Lifetime;

namespace FluidSimulation.Game;

public class ParticleStartupService(Particles particles) : IStartupService
{
    const int ParticleCount = 36;
    private const float ParticleRadius = 0.1f;
    private const float ParticleSpacing = 0.3f;
    
    private static readonly Vector2 Center = new(0f, 0f);
    
    public void Startup()
    {
        particles.Data = new ParticleData[ParticleCount];

        var cols = (int)Math.Sqrt(ParticleCount);
        var rows = (int)Math.Ceiling((float)ParticleCount / cols);
        
        for (var i = 0; i < ParticleCount; i++)
        {
            var col = i % cols;
            var row = i / cols;
            
            var x = Center.X + col * ParticleSpacing - cols / 2f * ParticleSpacing;
            var y = Center.Y + row * ParticleSpacing - rows / 2f * ParticleSpacing;
            
            particles.Data[i] = new ParticleData
            {
                Radius = ParticleRadius,
                X = x,
                Y = y,
                VelocityX = 0,
                VelocityY = 0,
                AccelerationX = 0,
                AccelerationY = 0
            };
        }
    }
}