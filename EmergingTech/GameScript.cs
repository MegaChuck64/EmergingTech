using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmergingTech
{
    public class GameScript
    {

        public string name = "";
        public string tag = "";
        public float layer = 0; 
        public List<Component> components = new List<Component>();
        public Transform transform;


        private Collider collider;

        public GameScript()
        {
            transform = new Transform(this);
            name = "New GameScript";

        }


        public T AddComponent<T>(T component) where T : Component
        {
            components.Add(component);
            return component;
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
        }


        public T GetComponent<T>() where T : Component
        {
            return components.Find(x => x is T) as T;
        }

        public void OnStart()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Start();
            }

            transform.Start();

            try
            {
                Start();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            GetCollider();

        }

        public Collider GetCollider()
        {
            if (collider == null)
                collider = GetComponent<Collider>();

            return collider;
        }

        public bool hasCollider
        {
            get
            {
                return collider != null;
            }
        }

        public static GameScript FindGameScript(string name)
        {
            return ScriptManager.GetScript(name);
        }


        public void OnUpdate(float dt)
        {
            foreach (UpdatableComponent comp in components.OfType<UpdatableComponent>())
            {
                comp.Update(dt);
            }

            transform.Update(dt);

            try
            {
                Update(dt);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }



        public void OnDraw(SpriteBatch sb)
        {
            foreach (DrawableComponent comp in components.OfType<DrawableComponent>())
            {
                comp.Draw(sb);
            }

            try
            {
                Draw(sb);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }



        //Script events
        public virtual void Start() { }

        public virtual void Update(float dt) { }

        public virtual void Draw(SpriteBatch sb) { }

        public virtual void OnCollision(GameScript other) { }
    }
}
