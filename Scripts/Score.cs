public class Score:GameScript
{

    Text txt;    
    public override void Start()
    {
        name = "Score";
        tag = "0";
        txt = new Text(this, "Score: " + tag);
        layer = 0.3f;
        transform.position = new Vector2(400,  20);
        
        AddComponent(new Sprite( 
                this, 
                Helper.Square( 80, new Color(0.2f, 0.2f, 0.2f, 0.4f) ), 
                new Vector2(-40, 0)));

        AddComponent(new Sprite( 
                this, 
                Helper.Square( 80, new Color(0.2f, 0.2f, 0.2f, 0.4f) ), 
                new Vector2(40, 0)));

        AddComponent(txt);
                

    }

    public override void Update(float dt)
    {

        txt.message = "Score: " + tag;
    }

    
}