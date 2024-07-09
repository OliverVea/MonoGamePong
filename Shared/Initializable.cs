namespace Shared;

public abstract class Initializable
{
    public virtual int InitializationPriority => 0;
    public abstract void Initialize();
}