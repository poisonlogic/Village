using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskTopVillage.GameClasses
{
    public static class InputController
    {
        private static bool WaitForKeys;
        private static int LastScrollWheelValue;
        public static void HandleKeyInput()
        {
            KeyboardState state = Keyboard.GetState();

            if (!state.GetPressedKeys().Any())
                WaitForKeys = false;

            if (WaitForKeys)
                return;

            if (state.GetPressedKeys().Any())
                WaitForKeys = true;

            if (state.IsKeyDown(Keys.W))
                MapRenderer.TryScroll(0, 10);
            if (state.IsKeyDown(Keys.A))
                MapRenderer.TryScroll(10, 0);
            if (state.IsKeyDown(Keys.S))
                MapRenderer.TryScroll(0, -10);
            if (state.IsKeyDown(Keys.D))
                MapRenderer.TryScroll(-10, 0);


            if (state.IsKeyDown(Keys.Right))
                DebugTool.CurrentX++;
            if (state.IsKeyDown(Keys.Left))
                DebugTool.CurrentX--;
            if (state.IsKeyDown(Keys.Up))
                DebugTool.CurrentY--;
            if (state.IsKeyDown(Keys.Down))
                DebugTool.CurrentY++;

            if (state.IsKeyDown(Keys.Enter))
                MapRenderer.MakeNewStructAtLocation(DebugTool.CurrentX, DebugTool.CurrentY);

        }

        public static void HandleMouseState()
        {
            var state = Mouse.GetState();
            if (LastScrollWheelValue < state.ScrollWheelValue)
                MapRenderer.TryZoom(-0.1);
            if (LastScrollWheelValue > state.ScrollWheelValue)
                MapRenderer.TryZoom(0.1);
            LastScrollWheelValue = state.ScrollWheelValue;
        }
    }
}
