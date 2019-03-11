public class Goal:GameScript
{
    public override void Start()
    {
        AddComponent(new Sprite(this, Helper.Square(64, Color.Yellow)));
        AddComponent(new Collider(this, new Rectangle(0,0,64,64)));
        transform.position = new Vector2(400, 10);
        layer = 0.2f;
    }

   public override void OnCollision(GameScript other)
    {
        if (other.name == "Player")
        {
            transform.position = Helper.RandomPointOnScreen();
            var gs = GameScript.FindGameScript("Score");
            gs.tag = (int.Parse(gs.tag) + 1).ToString();
        }
    }
}