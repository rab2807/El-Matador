using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrower : MonoBehaviour
{
    private Timer trackingTimer;
    private float trackingTime = 2f;
    private bool trackingActive;
    private Vector2 trackerFixedPosition;

    private int bombRound = 3;

    private GameObject player;
    private GameObject tracker;
    private bool isActive;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        tracker = GameObject.FindGameObjectWithTag("tracker");
        tracker.SetActive(false);

        trackingTimer = gameObject.AddComponent<Timer>();
        trackingTimer.TargetTime = trackingTime;
    }

    public void Initiate()
    {
        if (isActive)
            bombRound = 3;
        else
        {
            isActive = true;
            trackingTimer.ScheduleTask(StartTracking);
        }
    }

    private void ThrowBomb()
    {
        AudioManager.Play("shoot");

        GameObject bomb = GameManager.GetBomb();
        bomb.GetComponent<Bomb>().Initiate(player.transform.position);

        if (bombRound == 0)
        {
            isActive = false;
            gameObject.SetActive(false);
            return;
        }

        trackingTimer.ScheduleTask(trackingTime * 2, StartTracking);
    }

    void StartTracking()
    {
        tracker.SetActive(true);
        trackingActive = true;
        trackingTimer.ScheduleTask(() =>
        {
            trackingActive = false;
            ThrowBomb();
        });
    }

    void Update()
    {
        if (trackingActive)
            tracker.transform.position = player.transform.position;
    }
}