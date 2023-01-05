using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private Timer timer;
    private float duration = 10;
    
    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.TargetTime = duration;
    }

    public void Initiate()
    {
        
    }
    void Update()
    {
        
    }
}
