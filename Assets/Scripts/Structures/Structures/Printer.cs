

public class Printer : Producer
{
    public override void Start()
    {
        base.Start();
        input = new Cost();
        output = new Paper();
        maxProduceCooldown = 1f;
    }
}