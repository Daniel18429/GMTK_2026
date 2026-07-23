public abstract class StructureUpgrade
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public float Cost { get; protected set; }

    public int UpgradeLevel { get; protected set; } = 0;

    public virtual bool CanUpgrade(StructureParent structure)
    {
        if (!MoneyManager.HasMoney(Cost)) return false;
        return true;
    }

    public bool UpgradeStructure(StructureParent structure)
    {        
        if (CanUpgrade(structure))
        {
            OnUpgrade(structure);
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void OnUpgrade(StructureParent structure)
    {
        UpgradeLevel++;
    }
    
}