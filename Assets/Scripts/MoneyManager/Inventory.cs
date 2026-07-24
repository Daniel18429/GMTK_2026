using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
public class Inventory
{
    
    private Dictionary<Resource, int> _maxResources;
    public Dictionary<Resource, int> Resources { get; private set; } = new Dictionary<Resource, int>();

    public Inventory(Dictionary<Resource, int> maxResources)
    {
        _maxResources = maxResources;
        foreach (KeyValuePair<Resource, int> pair in _maxResources)
        {
            Resources.Add(pair.Key, 0);
        }
    }
    public bool AddResource(Resource resource)
    {
        Debug.Log("HJCIUOEWHJFIOJEW");
        if (!_maxResources.ContainsKey(resource)) return false;
        if (Resources[resource] < _maxResources[resource])
        {
            Resources[resource]++;
            Debug.Log(")(*@#$%^");
            return true;
        }

        return false;
    }

    public bool AddItem(Item item)
    {
        bool t = AddResource(item.resource);
        if(t) item.Consume();
        return t;
    }

    public void UseResource(Cost cost)
    {
        foreach (KeyValuePair<Resource, int> c in cost.costs)
        {
            Resources[c.Key] -= c.Value;
            if (Resources[c.Key] < 0) throw new Exception("Resource reduced beyond 0");
        }
    }
    
    public bool HasCost(Cost costs)
    {
        foreach (KeyValuePair<Resource, int> cost in costs.costs)
        {
            if (Resources[cost.Key] <  cost.Value)
            {
                return false;
            }
        }
        return true;
    }
}