public class PaperItem : Item
{
    public override void Start()
    {
        base.Start();
        resource = new Paper();
    }
}