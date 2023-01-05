using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrower : MonoBehaviour
{
    private Timer timer;
    private float interval = 1f;
    private int bombRound = 10;
    private GameObject player;
    private bool isActive;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timer = gameObject.AddComponent<Timer>();
        timer.TargetTime = interval;
    }

    public void Initiate()
    {
        if (isActive)
            bombRound = 10;
        else
        {
            isActive = true;
            timer.ScheduleTask(() => ThrowBomb());
        }
    }

    private void ThrowBomb()
    {
        GameObject bomb = GameManager.GetBomb();
        bomb.GetComponent<Bomb>().Initiate(gameObject, player);

        if (bombRound == 0)
        {
            isActive = false;
            gameObject.SetActive(false);
            return;
        }

        timer.ScheduleTask(() => ThrowBomb());
    }

    void Update()
    {
    }
}