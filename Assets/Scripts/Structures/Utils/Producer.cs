using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Producer : StructureParent
{
    private Inventory inventory;
    [SerializeField] protected Resource output;
    [SerializeField] protected Cost input;
    
    
    private float produceCooldown;
    protected float maxProduceCooldown;
    private Collider2D col;
    private Vector2 spawnDir;

    public virtual void Awake()
    {
        
    }

    public override void Start()
    {
        base.Start();
        col = GetComponent<Collider2D>();
        spawnDir = transform.right;
        inventory = new Inventory(input.costs);
    }

    public void FixedUpdate()
    {
        produceCooldown -= Time.fixedDeltaTime; 
        if (inventory.HasCost(input) && produceCooldown <= 0) Produce();
    }


    public void Produce()
    {
        Debug.Log("JIFOE$JWOFEW");
        inventory.UseResource(input);
        produceCooldown = maxProduceCooldown;
        Vector2 spawnPos = transform.position;
        Vector2 spawnOffset = new Vector2(spawnDir.x * col.bounds.size.x, spawnDir.y * col.bounds.size.y);
        spawnPos += spawnOffset;
        Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0, 360f));
        GameObject item = Instantiate(output.prefab, spawnPos, rotation);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Item")
        {
            Item item =  collision.gameObject.GetComponent<Item>();
            if (item != null)
            {
                Debug.Log(inventory.AddItem(item));
            }
        }
    }
}