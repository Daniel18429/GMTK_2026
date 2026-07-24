using System;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] public PlayerInfo PlayerInfo { get; private set; }
    [SerializeField] private PlayerValues _playerValues;

    public void Awake()
    {
        PlayerInfo = new PlayerInfo(this.gameObject, _playerValues);
        PlayerInfo.Init();
        
    }

    public void Update()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        PlayerInfo.Input.CacheInput(moveDirection, 
            Input.GetKey(KeyCode.Space), Input.GetKeyDown(KeyCode.E), 
            Input.GetMouseButtonDown(0), Input.GetKeyDown(KeyCode.R), 
            Input.GetKeyDown(KeyCode.Q), Input.GetKeyDown(KeyCode.P));
    }

    public void FixedUpdate()
    {
        PlayerInfo.FixedUpdate(Time.fixedDeltaTime);
    }
}