using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergingTech
{
    public class Collider : Component
    {

        private Rectangle rect;

        public Rectangle Rect
        {
            get
            {
                return new Rectangle(
                    (int)owner.transform.position.X + rect.X, 
                    (int)owner.transform.position.Y + rect.Y, 
                    (int)owner.transform.scale.X * rect.Width, 
                    (int)owner.transform.scale.Y * rect.Height);
            }
        }

        public Collider(GameScript _owner, Rectangle _rect)
        {
            owner = _owner;
            rect = _rect;
        }

        public override void Start()
        {
            if (rect == null)
            {
                Console.WriteLine("Collider with no rect exists");
            }
        }

    }
}
