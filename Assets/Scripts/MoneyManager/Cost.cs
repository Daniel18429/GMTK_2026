using System.Collections.Generic;

public class Cost
{

    public Cost(Dictionary<Resource, int> c)
    {
        this.costs = c;
    }
    public Dictionary<Resource, int> costs;
}