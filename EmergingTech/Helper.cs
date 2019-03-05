
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace EmergingTech
{
    public static class Helper
    {

        public static Game1 Game;

        public static Texture2D Square(int size, Color c)
        {
            Texture2D t = new Texture2D(Game.GraphicsDevice, size, size);

            Color[] cols = new Color[size * size];


            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    cols[size * x + y] = c;
                }
            }

            t.SetData<Color>(cols);

            return t;
        }

        public static Texture2D LoadTexture(string fileName)
        {

            Texture2D t = null;
            try
            {

                FileStream fileStream = new FileStream(@"..\..\..\..\..\Sprites\" + fileName + ".png", FileMode.Open);
                t = Texture2D.FromStream(Game.GraphicsDevice, fileStream);
                fileStream.Dispose();               
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return t;
        }


        public static Vector2 RandomPointOnScreen()

        {
            try
            {
                Random rand = new Random();

                int x = rand.Next(0, Game.Window.ClientBounds.Width);
                int y = rand.Next(0, Game.Window.ClientBounds.Height);
                return new Vector2(x, y);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return Vector2.Zero;
            }
        }

  
    }
}
