using System;
using UnityEngine;

public class MyTimer : MonoBehaviour
{

    public bool Done = false;
    public float TimeRemaining { get; private set; } = 0f;
    private float _timeMultiplier = 1;

    public void FixedUpdate()
    {
        this.Tick(Time.fixedDeltaTime);
        if (TimeRemaining <= 0)
        {
            Done = true;
        }
    }

    public void AdjustTime(float value)
    {
        if(TimeRemaining != Mathf.Infinity) TimeRemaining += value;
    }
    
    public void Tick(float deltaTime)
    {
        AdjustTime(-deltaTime * _timeMultiplier);
    }

    public void Reset(float timeRemaining)
    {
        TimeRemaining = timeRemaining;
        Done = false;
    }
}