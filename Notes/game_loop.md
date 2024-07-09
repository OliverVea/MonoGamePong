# Notes - Game Loop

## Game Loop

1. `if (Platform.BeforeRun()) BeginRun()`
2. `GraphicsDevice`created
3. `Platform.BeforeInitialize()`
4. `Initialize()`
5. `LoadContent()`
6. `IUpdateable` and `IDrawable` cleared
7. `BeginRun()`
8. *Loop starts*
9. If `IsFixedTimeStep`, wait for `TargetElapsedTime`
10. `Platform.BeforeUpdate()`
11. `Update()`
12. If draw is not suppressed, go to 12, else go to 8
13. `BeginDraw()`
14. `Draw()`
15. `EndDraw()`
16. *Loop repeats unless `Exit()` has been called*
17. `EndRun()`
18. `Exiting` event is raised
19. `UnloadContent()`

## Diagram

```mermaid
flowchart TD
    PlatformBeforeRun["<b>Platform.BeforeRun()</b>"]
    PlatformBeforeInitialize["<b>Platform.BeforeInitialize()</b>"]
    Initialize["<b>Initialize()</b>"]
    LoadContent["<b>LoadContent()</b>"]
    BeginRun["<b>BeginRun()</b>"]
    PlatformBeforeUpdate["<b>Platform.BeforeUpdate()</b>"]
    Update["<b>Update()</b>"]
    DrawSupressed?{{"<b>Draw</b> is suppressed?"}}
    BeginDraw["<b>BeginDraw()</b>"]
    Draw["<b>Draw()</b>"]
    EndDraw["<b>EndDraw()</b>"]
    ExitCalled?{{"<b>Exit()</b> called?"}}
    EndRun["<b>EndRun()</b>"]
    ExitingEvent["<b>Exiting</b> event is raised"]
    UnloadContent["<b>UnloadContent()</b>"]

    PlatformBeforeRun --> PlatformBeforeInitialize
    PlatformBeforeInitialize --> Initialize
    Initialize --> LoadContent
    LoadContent --> BeginRun
    BeginRun --> PlatformBeforeUpdate
    PlatformBeforeUpdate --> Update
    Update --> DrawSupressed?
    DrawSupressed? --> |true| ExitCalled?
    DrawSupressed? --> |false| BeginDraw
    BeginDraw --> Draw
    Draw --> EndDraw
    EndDraw --> ExitCalled?
    ExitCalled? --> |false| PlatformBeforeUpdate
    ExitCalled? --> |true| EndRun
    EndRun --> ExitingEvent
    ExitingEvent --> UnloadContent

    subgraph Game Loop
        PlatformBeforeUpdate
        Update
        DrawSupressed?
        BeginDraw
        Draw
        EndDraw
        ExitCalled?
    end
```
