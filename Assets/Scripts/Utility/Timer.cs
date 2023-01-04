using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float targetTime = -1;
    private bool isStarted;
    private bool isFinished;
    private Action func;

    public Timer()
    {
    }

    public Timer(float targetTime)
    {
        this.targetTime = targetTime;
    }

    public float TargetTime
    {
        get => targetTime;
        set
        {
            if (!isStarted) targetTime = value;
        }
    }

    public void ScheduleTask(float targetTime, Action func)
    {
        this.targetTime = targetTime;
        if (targetTime > 0) ScheduleTask(func);
        else Debug.Log("invalid time");
    }

    public void ScheduleTask(Action func)
    {
        this.func = func;
        StartTimer();
    }

    private void StartTimer()
    {
        if (!isStarted) StartCoroutine(SpendTime());
    }

    private IEnumerator SpendTime()
    {
        isStarted = true;
        yield return new WaitForSeconds(targetTime);
        isStarted = false;
        func();
    }
}