using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Threading.Tasks;

namespace EmergingTech
{

    public class Game1 : Game
    {
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public bool debug = false;

        System.Diagnostics.Stopwatch watch;


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

            CommandManager.command = new Command(this);

            Console.WriteLine($"Console Commands Initialized after: {watch.ElapsedMilliseconds} ms");


            ScriptManager.AddScripts();

            Console.WriteLine($"Scripts Added after: {watch.ElapsedMilliseconds} ms");





            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);



            UIManager.font = Content.Load<SpriteFont>(@"Fonts\consolas18");

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

            if (Input.keys.IsKeyDown(Keys.F10) && Input.lastKeys.IsKeyUp(Keys.F10))
            {
                ScriptManager.Reload();
            }

            if (debug)
            {
                CommandManager.HandleCommands();
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.OemTilde))
                {
                    debug = true;
                }

                ScriptManager.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }


            Input.End();

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            ScriptManager.Draw(spriteBatch);

            UIManager.Draw(spriteBatch);

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
