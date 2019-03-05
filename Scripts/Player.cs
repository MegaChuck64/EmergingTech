
public class Player : GameScript
{
    public override void Start()
    {
        texture = Helper.LoadTexture("player");
        speed = 260;
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
                
                var scoreScript = ScriptManager.GetScript("Score");
                if (scoreScript != null)
                {
                    var scr = int.Parse(scoreScript.tag) + 1;

                    ScriptManager.ChangeTag("Score", scr.ToString());
                }
            }
        }

        base.Update(dt);        
    }  
} 
