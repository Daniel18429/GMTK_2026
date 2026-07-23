using UnityEngine;
using System;
using System.Collections.Generic;

public class StructureParent : MonoBehaviour
{
    [SerializeField] protected float lifetime = Mathf.Infinity;
    private MyTimer _timer;
    public List<StructureUpgrade> upgrades { get; private set; } = new List<StructureUpgrade>();
    
    [SerializeField] private string description;
    public String Description { get => description; protected set => description = value; }
    

    public virtual void Start()
    {
        _timer = this.gameObject.AddComponent<MyTimer>();
        _timer.Reset(lifetime);
    }

    public virtual void Update()
    {
        if(_timer.Done) Destruct();
    }

    protected virtual void Destruct()
    {
        Destroy(this.gameObject);
    }

    public virtual void Repair(float amount)
    {
        lifetime += amount;
    }
    
}