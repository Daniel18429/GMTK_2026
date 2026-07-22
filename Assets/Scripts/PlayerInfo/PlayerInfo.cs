using System;
using UnityEngine;


[System.Serializable]
public class PlayerInfo
{
    public PlayerInput Input;
    private GameObject _player;
    public PlayerPhysics Physics;
    public PlayerValues Values;
    public PlayerContext Context;
    public StructureData StructureData;
    public PlayerInfo(GameObject gameObject, PlayerValues values)
    {
        _player = gameObject;
        Input = new PlayerInput();
        Physics = new PlayerPhysics();
        Context = new PlayerContext();
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
    }
}

public class PlayerContext
{
    public bool AirportBoosted = false;
}

public class StructureData
{
    public GameObject DisplayObj;
    public SpriteRenderer DisplayObjRenderer;
    public StructureObj CurrentStructureObj;
    public Sprite CurrentStructureObjSprite;

    public float Degrees;

    public void Start(GameObject displayObj)
    {
        DisplayObj = displayObj;
        DisplayObjRenderer = DisplayObj.GetComponent<SpriteRenderer>();
        CurrentStructureObjSprite = DisplayObj.GetComponent<SpriteRenderer>().sprite;
    }

    public void SetStruct(StructureObj s)
    {
        CurrentStructureObj = s;
        CurrentStructureObjSprite = s.prefab.GetComponent<SpriteRenderer>().sprite;
    }
}



public class PlayerInput 
{
    public Vector2 mouseDir;
    private Transform objToMouse;
    public Vector2 MoveDirection; 
    public bool DashPressed;
    public Vector2 lookDir = Vector2.right;
    public Vector2 mousePos = Vector2.zero;
    public float distToMouse = 0f;
    
    public void Init(GameObject player)
    {
        objToMouse = player.transform;
    }

    public void CacheInput(Vector2 moveDirection, bool dashPressed)
    {
        MoveDirection = moveDirection;
        if (!DashPressed) DashPressed = dashPressed;
        
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        mousePos = mouseWorldPos;
        distToMouse = Vector3.Distance(mousePos, objToMouse.position);
        mouseDir = (mouseWorldPos - objToMouse.position).normalized;

        if (moveDirection != Vector2.zero)
        {
            lookDir = moveDirection.normalized;
        }
    }

    public void Reset()
    {
        MoveDirection = Vector2.zero;
        DashPressed = false;
    }
}