using Microsoft.Xna.Framework;

namespace EmergingTech
{
    public class Transform : UpdatableComponent
    {

        public float speed;
        public Vector2 direction;
        public Vector2 position;
        public float rotation;
        public Vector2 scale;

        public Transform(GameScript _owner, Vector2? _position = null)
        {
            owner = _owner;
            position = (_position == null) ? Vector2.Zero : _position.Value;
            scale = Vector2.One;
        }

        public override void Start()
        {
            speed = 0f;
            direction = Vector2.Zero;
        }

        public override void Update(float dt)
        {
            position += direction * speed * dt;
        }
    }
}
