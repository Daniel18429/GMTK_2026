using TMPro;
using UnityEngine;


public delegate bool ButtonEvent(StructureParent structureParent);
public class UpgradeButton : MonoBehaviour
{
    public ButtonEvent OnClick;
    private SpriteRenderer spriteRenderer;
    
    private float signIntensity = 1;
    private float numShifts = 7;
    private float signMaxTime = 0.4f;
    private float signTime;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI Cost;

    private Color startColor;

    private Vector2 pos;

    public void Awake()
    {
        pos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Name = this.transform.Find("Canvas/Name").GetComponent<TextMeshProUGUI>();
        Debug.Log(Name == null);
        Debug.Log("AAAAAA");
        Description = this.transform.Find("Canvas/Description").GetComponent<TextMeshProUGUI>();
        Cost = this.transform.Find("Canvas/Cost").GetComponent<TextMeshProUGUI>();
        startColor = spriteRenderer.color;
    }

    public void Update()
    {
        if (signTime > 0)
        {
            pos = transform.position;
            Vector2 displayPos = pos;
            displayPos.x += ShakeAmount();
            transform.position = displayPos;
            signTime -= Time.deltaTime;
            if (signTime <= 0)
            {
                spriteRenderer.color = startColor;
            }
        }
    }

    public void Click(StructureParent structureParent)
    {
        if (OnClick == null) return;
        bool success = OnClick.Invoke(structureParent);
        if (!success)
        {
            ClickError();
        }
    }

    public void ClickError()
    {
        spriteRenderer.color = Color.red;
        signTime = signMaxTime;
    }
    
    private float ShakeAmount()
    {
        if (signTime < 0)
        {
            return 0;
        }
        else
        {
            return Mathf.Sin(Mathf.PI * signTime * numShifts / signMaxTime) * signTime * signIntensity / signMaxTime;
        }
    }
}