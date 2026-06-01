using System;
using UnityEngine;

[Serializable]
public class Timer
{
    protected float currentTime;
    
    public virtual void Update()
    {
        Update(Time.deltaTime);        
    }

    public virtual void Update(float timeAmount)
    {
        currentTime = currentTime - timeAmount;
    }

    public virtual bool Done()
    {
        return currentTime <= 0;
    }

    public virtual void SetTime(float time)
    {
        currentTime = time;
    }

    public float GetTimeLeft()
    {
        return currentTime;
    }
}

public class ActionTimer : Timer
{
    public Action callWhenDone = delegate{};
    bool calledDone = true;

    public override void Update(float timeAmount)
    {
        currentTime = currentTime - timeAmount;        
        if (currentTime <= 0)
        {
            if (calledDone == false)
            {
                calledDone = true;
                callWhenDone?.Invoke();
            }
        }
    }

    public override void SetTime(float time)
    {
        calledDone = false;
        base.SetTime(time);
    }
}
