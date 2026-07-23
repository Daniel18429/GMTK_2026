using UnityEngine;


public delegate bool ButtonEvent();
public class UpgradeButton : MonoBehaviour
{
    public ButtonEvent OnClick;
    private SpriteRenderer spriteRenderer;
    
    private float signIntensity = 1;
    private float numShifts = 7;
    private float signMaxTime = 0.4f;
    private float signTime;

    private Vector2 pos;

    public void Start()
    {
        pos = transform.position;
    }

    public void Update()
    {
        if (signTime > 0)
        {
            Vector2 displayPos = pos;
            displayPos.x += ShakeAmount();
            transform.position = displayPos;
            signTime -= Time.deltaTime;
            if (signTime <= 0)
            {
                spriteRenderer.color = Color.white;
            }
        }
    }

    public void Click()
    {
        if (OnClick == null) return;
        bool success = OnClick.Invoke();
        if (!success)
        {
            ClickError();
        }
    }

    public void ClickError()
    {
        spriteRenderer.color = Color.red;
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