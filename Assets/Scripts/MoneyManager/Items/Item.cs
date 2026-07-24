using System;
using UnityEngine;
public class Item : MonoBehaviour
{
    public Resource resource;
    public ItemPhysics Physics;

    public GameObject boostPad;

    public void Start()
    {
        Physics = new ItemPhysics(this.GetComponent<Rigidbody2D>());
    }
    
    

    public void FixedUpdate()
    {
        Physics.PhysicsUpdate();
    }
}