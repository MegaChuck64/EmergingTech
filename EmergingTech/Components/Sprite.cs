using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EmergingTech
{
    public class Sprite : DrawableComponent
    {
        public Texture2D texture;
        public Color tint;

        public Vector2 offset = Vector2.Zero;


        public Sprite(GameScript _owner, Texture2D _texture, Vector2? _offset = null)
        {
            owner = _owner;
            tint = Color.White;
            texture = _texture;
            offset = (_offset == null) ? Vector2.Zero : _offset.Value;
        }



        public override void Start()
        {
            if (owner == null)
                Console.WriteLine("Sprite has no owner.");


            if (texture == null)
                Console.WriteLine("Sprite has no texture.");

        }

        public override void Update(float dt)
        {
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, owner.transform.position + offset, null, tint, owner.transform.rotation, new Vector2(texture.Width / 2, texture.Height / 2), owner.transform.scale, SpriteEffects.None, owner.layer);
        }
    }
}
