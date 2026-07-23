using UnityEngine;
using System;
using Object = System.Object;

public class StructurePlacer : State<PlayerInfo>
{
    
    public StructurePlacer(StateMachine<PlayerInfo> machine, PlayerInfo info, State<PlayerInfo> parent) : base(machine, info, parent)
    {
    }

    protected override void OnEnter()
    {
        Debug.Log(_info.StructureData);
        Debug.Log(_info.StructureData.DisplayObj);
        _info.StructureData.DisplayObj.SetActive(true);
    }

    protected override void OnExit()
    {
        _info.StructureData.DisplayObj.SetActive(false);
    }

    protected override State<PlayerInfo> Transition() => null;
    
    protected override State<PlayerInfo> GetInitialState() => null;

    protected override void OnUpdate(float deltaTime) { }

    protected override void OnFixedUpdate(float deltaTime)
    {
        _info.StructureData.DisplayObjRenderer.sprite = _info.StructureData.CurrentStructureObj.sprite;
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
            StructureManager.Instance.Place(_info.StructureData.CurrentStructureObj, placementPos, rotation);
        }
    }
    
}