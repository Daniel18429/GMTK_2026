using UnityEngine;

public class MyTimer
{
    private float _endTime = float.NegativeInfinity;
    public float TimeRemaining => Mathf.Max(0f, _endTime - Time.time);
    public bool Done => Time.time >= _endTime;
    
    public void Reset(float duration)
    {
        _endTime = Time.time + duration;
    }

    public void End()
    {
        _endTime = Time.time;
    }
}