using UnityEngine;

public class Idle : State<PlayerInfo>
{
    public Idle(StateMachine<PlayerInfo> machine, PlayerInfo info, State<PlayerInfo> parent) : base(machine, info, parent)
    {
    }

    protected override void OnEnter()
    {
    }
    
    protected override void OnExit() { }

    protected override State<PlayerInfo> Transition()
    {
        Debug.Log("RAHHH");
        if (_info.Input.MoveDirection != Vector2.zero)
        {
            Debug.Log("RAHH RAH HRAHHA");
            return Machine.GetStateFromType<Walking>();
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
        _info.Physics.PlayerMoveVelocity = Vector2.zero;
    }
}

public class Walking : State<PlayerInfo>
{
    public Walking(StateMachine<PlayerInfo> machine, PlayerInfo info, State<PlayerInfo> parent) : base(machine, info, parent)
    {
    }
    
    protected override void OnEnter() { }
    
    protected override void OnExit() { }

    protected override State<PlayerInfo> Transition()
    {
        if (_info.Input.MoveDirection == Vector2.zero)
        {
            return Machine.GetStateFromType<Idle>();
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
        _info.Physics.PlayerMoveVelocity= _info.Input.MoveDirection * _info.Values.WalkSpeed;
    }
}