using UnityEngine;
using System;

public class StructurePlacement : State<PlayerInfo>
{
    
    public StructurePlacement(StateMachine<PlayerInfo> machine, PlayerInfo info, State<PlayerInfo> parent) : base(machine, info, parent)
    {
    }
    
    protected override void OnEnter() { }
    
    protected override void OnExit() { }

    protected override State<PlayerInfo> Transition() => null;
    
    protected override State<PlayerInfo> GetInitialState() => null;

    protected override void OnUpdate(float deltaTime) { }

    protected override void OnFixedUpdate(float deltaTime)
    {
        _info.StructureData.DisplayObjRenderer.sprite = _info.StructureData.CurrentStructureObjSprite;
        Vector2 placementPos = Vector2.zero;
        if (_info.Input.distToMouse > 5f)
        {
            placementPos = _info.Input.mouseDir * 5f;
        }
        else
        {
            placementPos = _info.Input.mousePos;
        }
        placementPos.x = Mathf.RoundToInt(placementPos.x);
        placementPos.y = Mathf.RoundToInt(placementPos.y);
        _info.StructureData.DisplayObj.transform.position = placementPos;
        _info.StructureData.DisplayObj.transform.
        
    }
}