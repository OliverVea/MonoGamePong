namespace Shared.Navigation;

public class NavigationPath
{
    public Id<NavigationPath> Id { get; } = Id<NavigationPath>.NewId();
    public required NavigationGraph Graph { get; init; }
    public required NavigationNode[] Nodes { get; init; }
}