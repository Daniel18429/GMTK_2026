using UnityEngine;
using System;

public class Structure : MonoBehaviour
{
    [SerializeField] protected float lifetime;
    private MyTimer _timer;

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
        
    }
}