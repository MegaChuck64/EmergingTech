using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmergingTech
{
    public static class CommandManager
    {
        public static Command command;

        public static void HandleCommands()
        {
            string line;
            if ((line = Console.ReadLine()) != null)
            {
                if (line.Contains("`"))
                {
                    Helper.Game.debug = false;
                }
                else
                {
                    var ls = line.Split('(', ')', ',', ';');

                    var method = ls[0];

                    List<string> param = new List<string>();
                    for (int i = 1; i < ls.Length; i++)
                    {
                        if (ls[i] != "")
                            param.Add(ls[i]);
                    }


                    MethodInfo mi;
                    if ((mi = typeof(Command).GetMethod(method)) != null)
                    {
                        bool pass = true;
                        try
                        {
                            mi.Invoke(command, param.ToArray());
                        }
                        catch (Exception e)
                        {
                            pass = false;
                            Console.WriteLine(e.Message);
                        }


                        command.game.debug = !pass;
                    }
                }
            }
        }
    }
}
