
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class AirportWalkPad : Structure
{
    private float _boostSpeed = 10.0f;
    private Vector2 _boostDirection;
    private MovementController controller;
    private List<Rigidbody2D> rigidbodies = new List<Rigidbody2D>();

    public override void Start()
    {
        base.Start();
        controller = GameObject.Find("Player").GetComponent<MovementController>();
        _boostDirection = this.gameObject.transform.up;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            controller._playerInfo.Physics.ExternalVelocity = _boostDirection * _boostSpeed;
        }
    }
}