using System;
using UnityEngine;


[System.Serializable]
public class PlayerInfo
{
    public PlayerInput Input;
    private GameObject _player;
    public PlayerPhysics Physics;
    public PlayerValues Values;
    public StructureData StructureData;
    public Context Context;
    public PlayerInfo(GameObject gameObject, PlayerValues values)
    {
        _player = gameObject;
        Input = new PlayerInput();
        Physics = new PlayerPhysics();
        Context = new Context();
        Values = values;
        StructureData = new StructureData();
    }

    public void Init()
    {
        Input.Init(_player);
        Physics.Init(_player.GetComponent<Rigidbody2D>());
    }

    public void FixedUpdate(float deltaTime)
    {
        Physics.PhysicsUpdate(deltaTime);
        Input.Reset();
    }
}

public class Context
{
    public GameObject currentBoostPad;
}

[System.Serializable]
public class StructureData
{
    [SerializeField] public GameObject DisplayObj;
    public SpriteRenderer DisplayObjRenderer;
    public StructureObj CurrentStructureObj;
    
    public float Degrees;

    public void Start(GameObject displayObj)
    {
        DisplayObj = displayObj;
        DisplayObjRenderer = DisplayObj.GetComponent<SpriteRenderer>();
    }

    public void SetStruct(StructureObj s)
    {
        CurrentStructureObj = s;
    }
}



public class PlayerInput 
{
    public Vector2 mouseDir;
    public Transform Player;
    public Vector2 MoveDirection; 
    public bool DashPressed;
    public Vector2 lookDir = Vector2.right;
    public Vector2 mousePos = Vector2.zero;
    public float distToMouse = 0f;
    public bool BuildModePressed = false;
    public bool PlaceStructure = false;
    public bool BuildRotate = false;
    
    public void Init(GameObject player)
    {
        Player = player.transform;
    }

    public void CacheInput(Vector2 moveDirection, bool dashPressed, bool buildModePressed, bool placeStructure, bool buildRotate)
    {
        MoveDirection = moveDirection;
        if (!DashPressed) DashPressed = dashPressed;
        if(!BuildModePressed) BuildModePressed = buildModePressed;
        if(!PlaceStructure) PlaceStructure = placeStructure;
        if(!BuildRotate) BuildRotate = buildRotate;
        
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        mousePos = mouseWorldPos;
        distToMouse = Vector3.Distance(mousePos, Player.position);
        mouseDir = (mouseWorldPos - Player.position).normalized;

        if (moveDirection != Vector2.zero)
        {
            lookDir = moveDirection.normalized;
        }
    }

    public void Reset()
    {
        MoveDirection = Vector2.zero;
        DashPressed = false;
        BuildModePressed = false;
        PlaceStructure = false;
        BuildRotate = false;
    }
}