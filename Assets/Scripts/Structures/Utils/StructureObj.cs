using UnityEngine;
using System.Collections;


[CreateAssetMenu(fileName = "StructureObj", menuName = "ScriptableObjects/StructureObj", order = 2)]
public class StructureObj : ScriptableObject
{
    [Header("Cost")]
    public float cost;
    [Header("Obj")]
    public GameObject prefab;
    public Sprite sprite => prefab.GetComponent<SpriteRenderer>().sprite;
}