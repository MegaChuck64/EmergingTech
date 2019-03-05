
public class Score : GameScript
{

   
    
    public override void Start()
    {
        tag = "0";
        position = new Vector2(300,0);
        texture = Helper.Square(64, new Color(0.2f, 0.2f, 0.2f, 0.4f));
        UIManager.AddText("Score", "Score: " + tag, new Vector2(position.X + 8,16), Color.Black);
        name = "Score";

    }

    public override void Draw(SpriteBatch sb)
    {
        UIManager.UpdateText("Score", "Score: " + tag);
        sb.Draw(texture, position + new Vector2(64,0), Color.White);
        base.Draw(sb);
    }
}
