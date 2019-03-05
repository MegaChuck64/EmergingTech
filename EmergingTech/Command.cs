using Microsoft.Xna.Framework;
using System;

namespace EmergingTech
{
    public class Command
    {
        public Game1 game;

        public Command(Game1 gme)
        {
            try
            {
                game = gme;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AddText(string txt, string x, string y)
        {
            try
            {
                UIManager.AddText(txt, new Vector2(float.Parse(x), float.Parse(y)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void AddRect(string name, string x, string y, string sze, string r, string g, string b)
        {
            try
            {
                var t = Helper.Square(int.Parse(sze), new Color(float.Parse(r), float.Parse(g), float.Parse(b)));

                GameScript go = new GameScript(t, new Vector2(float.Parse(x), float.Parse(y)))
                {
                    name = name
                };

                ScriptManager.gameScripts.Add(go);                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AddSpeed( string spd)
        {
            try
            {
                ScriptManager.gameScripts[ScriptManager.gameScripts.Count - 1].speed += float.Parse(spd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Clear()
        {
            try
            {
                ScriptManager.gameScripts.Clear();
                UIManager.TextObjects.Clear();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}