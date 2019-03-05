using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace EmergingTech
{
    public class GameScript : IGameScript
    {

        public float speed;
        public Vector2 direction;
        public Vector2 position;
        public Texture2D texture;
        public string name = "";
        public string tag = "";
       
        public Collision collision;

        public Rectangle Rect
        {
            get
            {
                if (texture != null)
                    return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                else
                    return new Rectangle((int)position.X, (int)position.Y, 1, 1);
            }
        }

        public GameScript()
        {
            speed = 0;
            direction = Vector2.Zero;
            position = Vector2.Zero;
            texture = Helper.Square(64, Color.Black);
        }

        public GameScript(Texture2D _texture, Vector2 _position)
        {
            texture = _texture;
            position = _position;
            speed = 0;
            direction = Vector2.Zero;

        }

        public virtual void Start()
        {
            if (texture == null)
            {
                System.Console.WriteLine("Game script with null texture has started.");
            }
        }



        public virtual void Update(float dt)
        {
            position += direction * speed * dt;
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }

    }
}
