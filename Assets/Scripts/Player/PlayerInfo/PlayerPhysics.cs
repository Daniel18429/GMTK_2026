using UnityEngine;
using System.Collections;
public class PlayerPhysics
{
    public Rigidbody2D Rb;
    public Vector2 PlayerMoveVelocity;
    public Vector2 ExternalVelocity;

    public void Init(Rigidbody2D rb)
    {
        Rb = rb;
    }

    public void PhysicsUpdate(float deltaTime)
    {
        Rb.velocity = PlayerMoveVelocity;
        Rb.velocity += ExternalVelocity;
        ExternalVelocity = PlayerMoveVelocity = Vector2.zero;
    }
}