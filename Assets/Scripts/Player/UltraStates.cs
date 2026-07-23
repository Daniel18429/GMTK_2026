

public class Moving : State<PlayerInfo>
{
    public Moving(StateMachine<PlayerInfo> machine, PlayerInfo info, State<PlayerInfo> parent) : base(machine, info, parent)
    {
    }

    protected override void OnEnter()
    {
    }
    
    protected override void OnExit() { }

    protected override State<PlayerInfo> Transition()
    {
        return null;
    }

    protected override State<PlayerInfo> GetInitialState() => Machine.GetStateFromType<Idle>();

    protected override void OnUpdate(float deltaTime) { }
    protected override void OnFixedUpdate(float deltaTime) { }
}