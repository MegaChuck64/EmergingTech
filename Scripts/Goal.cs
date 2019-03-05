
public class Goal : GameScript
{

    public override void Start()
    {
        texture = Helper.Square(64, Color.Green);

        position = Helper.RandomPointOnScreen();

        base.Start();

        name = "Goal";
        
    }
}
