using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergingTech
{
    public class TextObject
    {
        public SpriteFont font;
        public Vector2 position;
        public Color color;
        public string message;

        public TextObject(SpriteFont fnt, Vector2 pos, Color col, string msg)
        {
            font = fnt;
            position = pos;
            color = col;
            message = msg;
        }
    }

    public static class UIManager
    {
        public static Dictionary<string, TextObject> TextObjects = new Dictionary<string, TextObject>();
        public static SpriteFont font;

        public static void AddText(string name, TextObject textObject)
        {
            TextObjects.Add(name, textObject);
        }

        public static void AddText(string name, string msg, Vector2 position, Color col)
        {
            TextObject to = new TextObject(font, position, col, msg);

            TextObjects.Add(name, to);
        }

        public static void AddText(string msg, Vector2 position)
        {
            TextObject to = new TextObject(font, position, Color.Black, msg);

            TextObjects.Add(msg, to);
        }

        public static void AddText(string msg, Vector2 position, Color col)
        {
            TextObject to = new TextObject(font, position, col, msg);

            TextObjects.Add(msg, to);
        }




        public static void UpdateText(string name, string msg)
        {
            try
            {
                TextObjects[name].message = msg;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }


        public static void Draw(SpriteBatch sb)
        {
            foreach (var to in TextObjects)
            {
                sb.DrawString(to.Value.font, to.Value.message, to.Value.position, to.Value.color);
            }
        }

    }
}
