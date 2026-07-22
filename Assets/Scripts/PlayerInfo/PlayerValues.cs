using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerValues", menuName = "ScriptableObjects/PlayerValues", order = 1)]
public class PlayerValues : ScriptableObject
{
    [Header("Walking")] [SerializeField] private float walkSpeed;
    public float WalkSpeed => walkSpeed;

}