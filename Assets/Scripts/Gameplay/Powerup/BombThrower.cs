using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrower : MonoBehaviour
{
    private Timer timer;
    private float interval = 1f;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timer = gameObject.AddComponent<Timer>();
        timer.TargetTime = interval;
        timer.ScheduleTask(() => ThrowBomb());
    }

    private void ThrowBomb()
    {
        GameObject bomb = GameManager.GetBomb();
        bomb.GetComponent<Bomb>().Initiate(gameObject, player);
        
        timer.ScheduleTask(() => ThrowBomb());
    }
    void Update()
    {
        
    }
}
