using System;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    public static StructureManager Instance;
    private Vector3 boxPos;
    private Vector3 boxSize;

    public void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    public void Update()
    {
    }
    

    public bool Place(StructureObj structObj, Vector3 position, Quaternion rotation)
    {
        GameObject obj = structObj.prefab;
        if (!MoneyManager.HasMoney(structObj.cost)) return false;
        
        
        Vector2 placementPos = position;
        if (IsBlocked(obj, placementPos)) return false;
        GameObject finalObj = Instantiate(obj, placementPos, rotation);
        return true;
    }

    private bool IsBlocked(GameObject obj, Vector2 position)
    {
        Vector2 checkSize = GetObjectSize(obj) * 0.959f;
        Collider2D[] hits = Physics2D.OverlapBoxAll(position, checkSize, 0f);
        boxPos = position;
        boxSize = checkSize;
        
        foreach (Collider2D hit in hits)
        {
            Debug.Log(hit.gameObject.name);
            if (hit.transform.IsChildOf(obj.transform)) continue;
            return true;
        }
        return false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(boxPos, boxSize);
    }

    private Vector2 GetObjectSize(GameObject obj)
    {
        BoxCollider2D collider2D = obj.GetComponent<BoxCollider2D>();
        if (collider2D != null)
        {
            return collider2D.size;
        }
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            return spriteRenderer.bounds.size;
        }
        return Vector2.zero;
    }
    
}