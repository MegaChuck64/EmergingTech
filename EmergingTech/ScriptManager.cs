using CSScriptLibrary;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EmergingTech
{
    public static class ScriptManager
    {
        public static List<GameScript> gameScripts = new List<GameScript>();



        public static void Reload()
        {
            gameScripts.Clear();
            UIManager.TextObjects.Clear();
            AddScripts();
        }


        public static void ChangeTag(string scriptName, string tag)
        {
            for (int i = 0; i < gameScripts.Count; i++)
            {
                if (gameScripts[i].name == scriptName)
                {
                    gameScripts[i].tag = tag;
                }
            }
        }

        public static GameScript GetScript(string name)
        {
            for (int i = 0; i < gameScripts.Count; i++)
            {
                if (gameScripts[i].name == name)
                {
                    return gameScripts[i];
                }

            }

            return null;
        }

        private static async Task<string> ReadFileAsync(string filePath)
        {
            using (FileStream sourceStream = new FileStream(filePath,
                FileMode.Open, FileAccess.Read, FileShare.Read,
                bufferSize: 4096, useAsync: true))
            {
                StringBuilder sb = new StringBuilder();

                //                      4096
                byte[] buffer = new byte[0x1000];
                int numRead;
                while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string text = Encoding.UTF8.GetString(buffer, 0, numRead);
                    sb.Append(text);
                }

                return sb.ToString();
            }
        }

        public static async void AddScripts()
        {
            string[] names = Directory.GetFiles(@"..\..\..\..\..\Scripts\", "*.cs");

            foreach (var name in names)
            {

                string body = "";
                if (File.Exists(name) == false)
                {
                    Console.WriteLine("file not found: " + name);
                    continue;
                }
                else
                {
                    try
                    {
                        string text = 
                            "//css_ignore_namespace Microsoft.Xna.Framework " +
                        "\n//css_ignore_namespace Microsoft.Xna.Framework.Graphics " +
                        "\n//css_ignore_namespace Microsoft.Xna.Framework.Input " +
                        "\n//css_ref MonoGame.Framework.dll " +
                        "\nusing EmergingTech; " +
                        "\nusing Microsoft.Xna.Framework; " +
                        "\nusing Microsoft.Xna.Framework.Graphics; " +
                        "\nusing Microsoft.Xna.Framework.Input; \n";

                        text += await ReadFileAsync(name);

                        body = text;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                GameScript script = null;
                
                AsmHelper ass = null;
                try
                {

                    ass = new AsmHelper(CSScript.LoadCode(body, null, true));

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }


                IGameScript gs = null;
                try
                {

                    gs = (IGameScript)ass.CreateObject("*");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }


                script = gs as GameScript;


                if (script != null)
                {
                    script.name = name;
                    gameScripts.Add(script);
                }
            }


            Start();

        }

        public static void Start()
        {
            for (int i = 0; i < gameScripts.Count; i++)
            {
                gameScripts[i].Start();
            }

        }

        public static void Update(float elapsed)
        {
            for (int i = 0; i < gameScripts.Count; i++)
            {
                gameScripts[i].Update(elapsed);

                int cols = 0;

                for (int j = 0; j < gameScripts.Count; j++)
                {
                    if (gameScripts[i].name != gameScripts[j].name)
                    {
                        if (gameScripts[i].Rect.Intersects(gameScripts[j].Rect))
                        {
                            cols++;

                            Collision col = new Collision
                            {
                                colA = gameScripts[i],
                                colB = gameScripts[j]
                            };

                            gameScripts[i].collision = col;
                            gameScripts[j].collision = col;
                        }
                    }
                }

                if (cols == 0)
                {
                    gameScripts[i].collision = null;
                }
                else
                {

                }

            }
        }

        public static void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < gameScripts.Count; i++)
            {
                gameScripts[i].Draw(sb);
            }
        }


    }
}
