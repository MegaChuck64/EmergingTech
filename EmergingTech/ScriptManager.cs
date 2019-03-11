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
            AddScripts();
        }


        public static void ChangeTag(string scriptName, string tag)
        {
            try
            {
                gameScripts.Find(x => x.name == scriptName).tag = tag;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static GameScript GetScript(string name)
        {
            try
            {
                return gameScripts.Find(x => x.name == name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }


        /// <summary>
        /// Return file at path as string async
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static async Task<string> ReadFileAsync(string filePath)
        {
            //create stream with path
            using (FileStream stream = new FileStream(filePath,
                FileMode.Open, FileAccess.Read, FileShare.Read,
                bufferSize: 4096, useAsync: true))
            {

                StringBuilder sb = new StringBuilder();

                //                      4096
                byte[] buffer = new byte[0x1000];
                int numRead;
                //read stream into buffer
                while ((numRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string text = Encoding.UTF8.GetString(buffer, 0, numRead);
                    sb.Append(text);
                }

                return sb.ToString();
            }
        }

        public static async void AddScripts()
        {
            //Get every c# file in our scripts folder
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
                    // I use CSSCript to load the cs files which has its own tag for adding or removing refrences

                    // CSScript uses reflection to create a temporary type which creates refrence conflicts, so i
                    // force only one refrence to most xna libraries and a refrence to this namespace using the css_ tags
                    try
                    {
                        string header =
                            "//css_ignore_namespace Microsoft.Xna.Framework " +
                        "\n//css_ignore_namespace Microsoft.Xna.Framework.Graphics " +
                        "\n//css_ignore_namespace Microsoft.Xna.Framework.Input " +
                        "\n//css_ref MonoGame.Framework.dll " +
                        "\nusing EmergingTech; " +
                        "\nusing Microsoft.Xna.Framework; " +
                        "\nusing Microsoft.Xna.Framework.Graphics; " +
                        "\nusing Microsoft.Xna.Framework.Input; \n";

                        body = await ReadFileAsync(name);

                        body = header + body;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }


                //Used to create dynamic assemblies
                AsmHelper assembly = null;
                try
                {
                    //Load code into our assembly and put this assembly on our app domain
                    // use of 'using' to make sure we dispose our temp files 
                    using (assembly = new AsmHelper(CSScript.LoadCode(body, null, true)))
                    {
                        //create our object from our assembly as a gamescript 
                        GameScript script = (GameScript)assembly.CreateObject("*");
                        script.name = name;
                        gameScripts.Add(script);

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }



            }


            Start();

        }

        public static void Start()
        {
            for (int i = 0; i < gameScripts.Count; i++)
            {
                gameScripts[i].OnStart();
            }

        }

        public static void Update(float elapsed)
        {
            for (int i = 0; i < gameScripts.Count; i++)
            {
                gameScripts[i].OnUpdate(elapsed);


                //collision check
                int cols = 0;

                for (int j = 0; j < gameScripts.Count; j++)
                {

                    //dont check collision with self
                    //this means ojects that have the same name cant collide
                    if (gameScripts[i].name != gameScripts[j].name)
                    {

                        if (!gameScripts[i].hasCollider || !gameScripts[j].hasCollider) continue;

                        //check if rectangles intersect
                        if (gameScripts[i].GetCollider().Rect.Intersects(gameScripts[j].GetCollider().Rect))
                        {
                            cols++;

                            Collision col = new Collision
                            {
                                colA = gameScripts[i],
                                colB = gameScripts[j]
                            };

                            //pass other object
                            gameScripts[i].OnCollision(gameScripts[j]);
                            gameScripts[j].OnCollision(gameScripts[i]);
                        }
                    }
                }

            }
        }

        public static void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < gameScripts.Count; i++)
            {
                gameScripts[i].OnDraw(sb);
            }
        }


    }
}
