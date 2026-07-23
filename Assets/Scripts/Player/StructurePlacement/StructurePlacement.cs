using UnityEngine;
using System;
using Object = System.Object;

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
            Vector2 pos = (_info.Input.mouseDir * 5f) + new Vector2(_info.Input.Player.position.x, _info.Input.Player.position.y);
            placementPos = pos;
        }
        else
        {
            placementPos = _info.Input.mousePos;
        }
        placementPos.x = Mathf.RoundToInt(placementPos.x);
        placementPos.y = Mathf.RoundToInt(placementPos.y);
        _info.StructureData.DisplayObj.transform.position = placementPos;
        Quaternion rotation = Quaternion.Euler(0, 0, _info.StructureData.Degrees);
        _info.StructureData.DisplayObj.transform.rotation = rotation;


        if (_info.Input.PlaceStructure)
        {
            PlaceStructure(placementPos, rotation);
        }
    }

    private void PlaceStructure(Vector3 placementPos, Quaternion placementRot)
    {
        GameObject go = GameObject.Instantiate(_info.StructureData.CurrentStructureObj.prefab, placementPos, placementRot);
    }
}