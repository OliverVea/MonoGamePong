using Entombed.Game.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shared.Camera;
using Shared.Content;

namespace Entombed.Game.Levels;

public class LevelGuiService(IsometricCamera isometricCamera, GuiState guiState, RoomLookup roomLookup, RoomGraph roomGraph, ContentLookup<SpriteFont> fontLookup)
{
    private const float LineThickness = 0.5f;
    
    public void DrawGui(SpriteBatch spriteBatch)
    {
        if (!guiState.ShowLevel) return;

        DrawRoomSteps(spriteBatch);
        DrawRoomConnections(spriteBatch);
    }

    private void DrawRoomSteps(SpriteBatch spriteBatch)
    {
        var font = fontLookup.Get(Ids.Arial);
        
        foreach (var room in roomLookup.Values)
        {
            var roomCenter = room.Origin;
            var roomScreenPosition = isometricCamera.WorldToScreen(roomCenter);
            
            var stepsFromStart = roomGraph.GetStepsFromStart(room.Id);
            var text = stepsFromStart.ToString();
            
            spriteBatch.DrawString(font, text, roomScreenPosition, Color.White);
        }
    }

    private void DrawRoomConnections(SpriteBatch spriteBatch)
    {
        foreach (var room in roomLookup.Values)
        {
            var roomCenter = room.Origin;
            var roomScreenPosition = isometricCamera.WorldToScreen(roomCenter);
            
            foreach (var adjacentRoomId in roomGraph.GetAdjacentRooms(room.Id))
            {
                var adjacentRoom = roomLookup.Get(adjacentRoomId);
                var adjacentRoomCenter = adjacentRoom.Origin;
                var adjacentRoomScreenPosition = isometricCamera.WorldToScreen(adjacentRoomCenter);
                
                spriteBatch.DrawLine(roomScreenPosition, adjacentRoomScreenPosition, Color.White, LineThickness);
            }
        }
    }
}