namespace Shared.Lifetime;

/// <summary>
/// Represents a service that is started once when the game starts.
/// </summary>
/// <remarks>
/// <b>IStartup services cannot depend on scoped services.</b>
/// </remarks>
public interface IStartupService
{
    /// <summary>
    /// Called once when the game starts.
    /// </summary>
    void Startup();

    public int StartupPriority => 0;
    bool Active => true;
}