using System;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    public static StructureManager Instance;
    private Vector3 boxPos;
    private Vector3 boxSize;
    private GameObject displayObj;

    private float signIntensity = 1;
    private float numShifts = 7;
    private float signMaxTime = 0.4f;
    private float signTime;

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

        displayObj = new GameObject("Display");
        displayObj.AddComponent<SpriteRenderer>();
    }

    public void Update()
    {
        signTime -= Time.deltaTime;
        if (signTime < 0)
        {
            displayObj.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void RenderStructure(StructureObj obj, Vector3 position, Quaternion rotation)
    {
        Vector3 displayPos = position;
        displayPos.z = -1;
        if (signTime > 0)
        {
            Debug.Log("RAHH");
            float amount = Mathf.Sin(Mathf.PI * signTime * numShifts / signMaxTime) * signTime * signIntensity / signMaxTime;
            Debug.Log(amount);
            displayPos.x += amount;
        }
        displayObj.transform.position = displayPos;
        displayObj.transform.rotation = rotation;
        displayObj.GetComponent<SpriteRenderer>().sprite = obj.sprite;
    }

    public bool Place(StructureObj structObj, Vector3 position, Quaternion rotation)
    {
        GameObject obj = structObj.prefab;
        Debug.Log(obj.GetComponent<Collider2D>() == null);
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
            IncorrectPlacement();
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

    public void IncorrectPlacement()
    {
        signTime = signMaxTime;
        displayObj.GetComponent<SpriteRenderer>().color = Color.red;
    }
}