using System;
using UnityEngine;

public class Dash : State<PlayerInfo>
{
    private Vector2 _dashDir;
    private float _dashSpeed;
    private float _dashTime;
    private MyTimer _dashTimer = new MyTimer();
    
    public Dash(StateMachine<PlayerInfo> machine, PlayerInfo info, State<PlayerInfo> parent) : base(machine, info, parent)
    {
    }

    protected override void OnEnter()
    {
        
        _dashSpeed = _info.Val.DashDistance / _info.Val.DashTime;
        _dashTime =  _info.Val.DashTime;


        _dashDir = _info.Input.lookDir.normalized;
        _dashTimer.Reset(_dashTime);
        _info.Physics.Gravity = 0;
    }

    protected override void OnExit()
    {
        _info.Timers.DashCooldown.Reset(2.0f);
        _info.Physics.Rigidbody2D.velocity = Vector2.zero;
    }

    protected override State<PlayerInfo> Transition()
    {
        if (_dashTimer.Done)
        {
            return Machine.GetStateFromType<Airborne>();
        }
        else
        {
            return null;
        }
    }
    
    protected override State<PlayerInfo> GetInitialState() => null;

    protected override void OnUpdate(float deltaTime) { }

    protected override void OnFixedUpdate(float deltaTime)
    {
        _info.Physics.Rigidbody2D.velocity = _dashDir * _dashSpeed;
    }
}