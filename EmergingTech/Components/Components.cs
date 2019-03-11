using Microsoft.Xna.Framework.Graphics;


namespace EmergingTech
{
    public abstract class Component
    {
        public GameScript owner;
        public abstract void Start();
    }

    public abstract class UpdatableComponent : Component
    {
        public abstract void Update(float dt);
    }

    public abstract class DrawableComponent : UpdatableComponent
    {
        public abstract void Draw(SpriteBatch sb);
    }
}
