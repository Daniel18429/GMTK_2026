

using System.Collections.Generic;

public class Printer : Producer
{
    
    public override void Awake()
    {
        input = new Cost(new Dictionary<Resource, int>());
    }
    public override void Start()
    {
        base.Start();
        output = new Paper();
        maxProduceCooldown = 1f;
    }
}