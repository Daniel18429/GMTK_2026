using UnityEngine;

public class WalkPadSpeedBoost : StructureUpgrade
{
    private float boostUpgradeAmount = 1f;
    public WalkPadSpeedBoost()
    {
        Name = "Speed Boost!";
        Description = "Increases speed of walk pad";
        Cost = 2f;
    }
    public override void OnUpgrade(StructureParent structure)
    {
        base.OnUpgrade(structure);
        Cost += 1f;
        AirportWalkPad walkPad = (AirportWalkPad)structure;
        walkPad.UpgradeBoost(boostUpgradeAmount);
    }
}