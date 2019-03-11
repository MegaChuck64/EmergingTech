using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmergingTech
{

    public class Command
    {
        private static Command com = null;

        public static Command COM
        {
            get
            {
                if (com == null)
                {
                    com = new Command();
                }
                return com;
            }
        }


        public static void HandleCommands()
        {
            string line;
            if ((line = Console.ReadLine()) != null)
            {
                if (line.Contains("`"))
                {
                    Helper.Game.state = Game1.GameState.Playing;
                }
                else
                {
                    var ls = line.Split('(', ')', ',', ';');

                    var method = ls[0];

                    List<string> param = new List<string>();
                    for (int i = 1; i < ls.Length; i++)
                    {
                        if (ls[i] != "" && ls[i] != " ")
                            param.Add(ls[i]);
                    }


                    MethodInfo mi;
                    if ((mi = typeof(Command).GetMethod(method)) != null)
                    {
                        bool pass = true;
                        try
                        {
                            mi.Invoke(COM, param.ToArray());
                        }
                        catch (Exception e)
                        {
                            pass = false;
                            Console.WriteLine(e.Message);
                        }

                        
                        Helper.Game.state = (pass) ? Game1.GameState.Playing : Game1.GameState.Paused;
                    }
                }
            }
        }


        public void AddText(string msg, string x, string y, string r, string g, string b, string a)
        {
        
            GameScript gs = new GameScript();
            Text txt = new Text(gs, msg, new Color(int.Parse(r), int.Parse(g), int.Parse(b), int.Parse(a)), Helper.Game.font);
            gs.transform.position = new Vector2(int.Parse(x), int.Parse(y));


            gs.AddComponent(txt);
            gs.name = msg;

            ScriptManager.gameScripts.Add(gs);
               
        }

        public void Clear()
        {
            ScriptManager.gameScripts.Clear();
        }

        public void LoadScripts()
        {
            ScriptManager.Reload();
        }

    }
}
