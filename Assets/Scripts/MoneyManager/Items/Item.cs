using System;
using UnityEngine;
public class Item : MonoBehaviour
{
    public Resource resource { get; protected set; }
    public ItemPhysics Physics;

    public GameObject boostPad;

    public virtual void Start()
    {
        Physics = new ItemPhysics(this.GetComponent<Rigidbody2D>());
    }

    public void Consume()
    {
        Destroy(this.gameObject);
    }
    
    public void FixedUpdate()
    {
        Physics.PhysicsUpdate();
    }
}