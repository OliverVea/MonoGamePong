﻿using System.Diagnostics.CodeAnalysis;

namespace Entombed.Game.Levels;

[method: SetsRequiredMembers]
public class DoorOpenedEvent(Id<Door> id)
{
    public required Id<Door> Id { get; init; } = id;
}