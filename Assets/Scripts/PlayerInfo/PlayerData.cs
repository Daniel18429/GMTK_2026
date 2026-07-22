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
}