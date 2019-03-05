using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergingTech
{
    public static class Input
    {
        public static KeyboardState keys;
        public static KeyboardState lastKeys;

        public static MouseState mouse;
        public static MouseState lastMouse;

        public static void Begin()
        {
            keys = Keyboard.GetState();
            mouse = Mouse.GetState();
        }

        public static void End()
        {
            lastKeys = keys;
            lastMouse = mouse;
        }

        public static float GetAxis(string axis)
        {
            float val = 0f;
            switch (axis)
            {
                case "Vertical":
                    if (keys.IsKeyDown(Keys.W) || keys.IsKeyDown(Keys.Up))
                    {
                        val = 1f;
                    }
                    else if (keys.IsKeyDown(Keys.S) || keys.IsKeyDown(Keys.Down))
                    {
                        val = -1f;
                    }
                    break;
                case "Horizontal":
                    if (keys.IsKeyDown(Keys.A) || keys.IsKeyDown(Keys.Left))
                    {
                        val = -1f;
                    }
                    else if (keys.IsKeyDown(Keys.D) || keys.IsKeyDown(Keys.Right))
                    {
                        val = 1f;
                    }
                    break;
            }

            return val;
        }
        
    }
}
