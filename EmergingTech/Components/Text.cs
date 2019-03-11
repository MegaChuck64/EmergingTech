using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EmergingTech
{
    public class Text : DrawableComponent
    {
        public string message;

        public SpriteFont font;

        public Color color;

        public Vector2 offset = Vector2.Zero;

        public Text(GameScript _owner, string _message, Color? _color = null, SpriteFont _font = null)
        {
            owner = _owner;
            message = _message;
            font = (_font == null) ? Helper.Game.font : _font;
            color = (_color == null) ? Color.White : _color.Value;
        }

        public override void Start()
        {
        }

        public override void Update(float dt)
        {
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.DrawString(font, message, owner.transform.position + offset, color, owner.transform.rotation, font.MeasureString(message)/2, owner.transform.scale, SpriteEffects.None, owner.layer);
        }
    }
}
