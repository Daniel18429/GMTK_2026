using UnityEngine;

public class RepairUpgrade : StructureUpgrade
{
    public float RepairRate = 10f;

    public RepairUpgrade()
    {
        Name = "Wind Clock";
        Description = "Increases countdown of structure";
        Cost = RepairRate;
    }

    public override void OnUpgrade(StructureParent structure)
    {
        structure.Repair(RepairRate);
    }
}