using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chainsaw : MonoBehaviour
{
    private Timer timer;
    private float interval = 1f;
    private bool isOn;
    private int count = 3;

    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.TargetTime = interval;

        isOn = true;
        timer.ScheduleTask(()=>Toggle());
    }

    void Toggle()
    {
        isOn = !isOn;
        timer.ScheduleTask(()=>Toggle());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOn)
        {
            if (other.gameObject.GetComponent<Player>() != null)
            {
                // decrease life
                count--;
                if(count==0)
                    GameManager.ReturnChainsaw(gameObject);
            }
            else if (other.gameObject.GetComponent<Villain>() != null)
            {
                // decrease life
                count--;
                if(count==0)
                    GameManager.ReturnChainsaw(gameObject);
            }
        }
    }
}
