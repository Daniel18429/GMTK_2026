using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


[System.Serializable]
public class PlayerInfo
{
    public PlayerInput Input;
    public GameObject Player;
    public PlayerPhysics Physics;
    public PlayerValues Values;
    public StructureData StructureData;
    public Context Context;
    public PlayerInfo(GameObject gameObject, PlayerValues values)
    {
        Player = gameObject;
        Input = new PlayerInput();
        Physics = new PlayerPhysics();
        Context = new Context();
        Values = values;
        StructureData = new StructureData();
    }

    public void Init()
    {
        Input.Init(Player);
        Physics.Init(Player.GetComponent<Rigidbody2D>());
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
    public bool Interact = false;
    public bool EditorModePressed = false;
    public bool BuildRotate = false;
    
    public void Init(GameObject player)
    {
        Player = player.transform;
    }

    public void CacheInput(Vector2 moveDirection, bool dashPressed, bool buildModePressed, bool placeStructure, bool buildRotate, bool editorModePressed)
    {
        MoveDirection = moveDirection;
        if (!DashPressed) DashPressed = dashPressed;
        if(!BuildModePressed) BuildModePressed = buildModePressed;
        if(!Interact) Interact = placeStructure;
        if(!BuildRotate) BuildRotate = buildRotate;
        if(!EditorModePressed) EditorModePressed = editorModePressed;
        
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
        Interact = false;
        BuildRotate = false;
        EditorModePressed = false;
    }

    public GameObject GetObjectClicked()
    {
        GameObject go = null;
        
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            go = hit.collider.gameObject;
        }
        
        return go;
    }
}