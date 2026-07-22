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
            Node<Idle>(),
            Node<Walking>(),
            Node<StructurePlacement>()
        };
        StateMachineBuilder<PlayerInfo> builder = new StateMachineBuilder<PlayerInfo>(_stateMachine, _playerInfo);
        builder.BuildTree(children);
        _stateMachine.Initialize(_stateMachine.GetStateFromType<StructurePlacement>());
        _playerInfo.StructureData.Start(displayObj);
        _playerInfo.StructureData.SetStruct(structureObj);
    } 
    

    public void Update()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _playerInfo.Input.CacheInput(moveDirection, 
            Input.GetKey(KeyCode.Space));
        _stateMachine.Update(Time.deltaTime);
    }

    public void FixedUpdate()
    {
        _stateMachine.FixedUpdate(Time.fixedDeltaTime);
        _playerInfo.FixedUpdate(Time.fixedDeltaTime);
    }
}