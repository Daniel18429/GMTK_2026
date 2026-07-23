using UnityEngine;

using static Tree<PlayerInfo>;

public class MovementController : MonoBehaviour
{
    [SerializeField] public PlayerInfo _playerInfo { get; private set; }
    [SerializeField] private StateMachine<PlayerInfo> _stateMachine = new StateMachine<PlayerInfo>();
    [SerializeField] private StructureObj structureObj;
    [SerializeField] private GameObject displayObj;
    
    public void Start()
    {
        _playerInfo = GetComponent<PlayerData>().PlayerInfo;
        StateNode<PlayerInfo>[] children =
        {
            Node<Moving>(
                Node<Idle>(),
                Node<Walking>())
        };
        StateMachineBuilder<PlayerInfo> builder = new StateMachineBuilder<PlayerInfo>(_stateMachine, _playerInfo);
        builder.BuildTree(children);
        _stateMachine.Initialize(_stateMachine.GetStateFromType<Moving>());
        _playerInfo.StructureData.Start(displayObj);
        _playerInfo.StructureData.SetStruct(structureObj);
    }

    public void Update()
    {
        _stateMachine.Update(Time.deltaTime);
    }

    public void FixedUpdate()
    {
        _stateMachine.FixedUpdate(Time.fixedDeltaTime);
    }
}