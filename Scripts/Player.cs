
public class Player : GameScript
{
    public override void Start()
    {
        AddComponent(new Sprite(this, Helper.LoadTexture("player")));
        AddComponent(new Collider(this, new Rectangle(0,0,64,64)));
        transform.speed = 250;        
        name = "Player";
        layer = 0.1f;

    }

    public override void Update(float dt)
    {
        transform.direction.X = Input.GetAxis("Horizontal");
        transform.direction.Y = -Input.GetAxis("Vertical");        
    }

} 
