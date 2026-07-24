using UnityEngine;

public class ItemPhysics
{
    
    public Vector2 velocity;
    public Rigidbody2D _rb;

    public ItemPhysics(Rigidbody2D rb)
    {
        _rb = rb;
    }
    public void PhysicsUpdate()
    {
        _rb.velocity = velocity;
        velocity = Vector2.zero;
    }
}