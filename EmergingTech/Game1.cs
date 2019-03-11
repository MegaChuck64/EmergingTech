using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Threading.Tasks;

namespace EmergingTech
{

    public class Game1 : Game
    {
        public enum GameState
        {
            Loading, Playing, Paused, Closing
        }

        public GameState state = GameState.Loading;

        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        System.Diagnostics.Stopwatch watch;

        public SpriteFont font;

        public Game1()
        {
            watch = new System.Diagnostics.Stopwatch();

            watch.Start();


            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            IsMouseVisible = true;

            Helper.Game = this;



            Console.WriteLine($"Console Commands Initialized after: {watch.ElapsedMilliseconds} ms");


            ScriptManager.AddScripts();

            Console.WriteLine($"Scripts Added after: {watch.ElapsedMilliseconds} ms");





            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);



            font = Content.Load<SpriteFont>(@"Fonts\consolas18");

            Console.WriteLine($"Fonts Loaded after: {watch.ElapsedMilliseconds} ms");




            graphics.PreferredBackBufferWidth = GraphicsDevice.Viewport.Bounds.Width / 2;
            graphics.PreferredBackBufferHeight = GraphicsDevice.Viewport.Bounds.Height / 2;

            Window.Position = new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);


            Console.WriteLine($"Windows Positioned after: {watch.ElapsedMilliseconds} ms");

        }

        protected override void UnloadContent()
        {

        }



        protected override void Update(GameTime gameTime)
        {
            Input.Begin();

            if (Input.keys.IsKeyDown(Keys.Escape)) Exit();

            switch (state)
            {
                case GameState.Loading:
                    if (ScriptManager.gameScripts.Count != 0)
                    {
                        state = GameState.Playing;
                        break;
                    }


                    if (Input.keys.IsKeyDown(Keys.F10) && Input.lastKeys.IsKeyUp(Keys.F10))
                    {
                        ScriptManager.Reload();
                    }

                    break;
                case GameState.Playing:

                    if (Input.keys.IsKeyDown(Keys.F10) && Input.lastKeys.IsKeyUp(Keys.F10))
                    {
                        ScriptManager.Reload();
                        state = GameState.Loading;
                        break;
                    }

                    if (Input.keys.IsKeyDown(Keys.OemTilde) && Input.lastKeys.IsKeyUp(Keys.OemTilde))
                    {
                        state = GameState.Paused;
                        break;
                    }


                    ScriptManager.Update((float)gameTime.ElapsedGameTime.TotalSeconds);



                    break;
                case GameState.Paused:
                    Command.HandleCommands();
                    break;
                case GameState.Closing:
                    break;
                default:
                    break;
            }




            Input.End();

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront);

            ScriptManager.Draw(spriteBatch);


            spriteBatch.End();



            if (watch.IsRunning)
            {
                watch.Stop();

                Console.WriteLine($"Total Start Time: {watch.ElapsedMilliseconds} ms");
            }

            base.Draw(gameTime);
        }
    }













}
