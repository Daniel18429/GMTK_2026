using System.Collections.Generic;
using UnityEngine;

public class TestPaperConsumer : Producer
{

    public override void Awake()
    {
        Dictionary<Resource, int> costs = new Dictionary<Resource, int>();
        costs.Add(new Paper(), 1);
        input = new Cost(costs);
        Debug.Log(input.costs);
    }
    public override void Start()
    {
        base.Start();
        output = new Paper();
        maxProduceCooldown = 1f;
    }

}