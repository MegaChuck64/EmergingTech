
public class Player : GameScript
{
    public override void Start()
    {
        texture = Helper.Square(64, Color.Blue);
        speed = 160;
        base.Start();
        
        name = "Player";
    }

    public override void Update(float dt)
    {
        direction.X = Input.GetAxis("Horizontal");
        direction.Y = -Input.GetAxis("Vertical");

        if (collision != null)
        {
            var other = collision.GetOther(this); 
            if (other.name == "Goal")
            {
                other.position = Helper.RandomPointOnScreen();
                
                var scr = int.Parse(ScriptManager.GetScript("Score").tag) + 1;

                ScriptManager.ChangeTag("Score", scr.ToString());
            }
        }

        base.Update(dt);        
    }  
} 
