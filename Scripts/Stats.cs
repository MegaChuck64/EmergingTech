
public class Stats : GameScript
{

    public override void Start()
    {
        texture = Helper.Square(42, Color.Green);

        position = Helper.RandomPointOnScreen();


        name = "Goal";

                
    }
}
