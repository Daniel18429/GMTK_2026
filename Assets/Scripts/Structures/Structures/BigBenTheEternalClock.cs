using UnityEngine;

public class BigBenTheEternalClock : StructureParent
{
    public override void Start()
    {
        base.Start();
        upgrades.Add(new RepairUpgrade());
    }
}