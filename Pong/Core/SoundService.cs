using Microsoft.Xna.Framework.Audio;
using Shared.Content;
using Shared.Events;
using Shared.Lifetime;

namespace Pong.Core;

public class SoundService(IEventObserver<BallHitObjectEvent> ballHitObjectEventObserver, ContentLookup<SoundEffect> songLookup) : IStartupService
{
    public void Startup()
    {
        ballHitObjectEventObserver.Subscribe(OnBallHitObject);
    }

    private void OnBallHitObject(BallHitObjectEvent obj)
    {
        var pong = songLookup.Get(Ids.Pong);
        pong.Play();
    }
}