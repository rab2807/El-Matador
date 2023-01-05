using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletThrower : MonoBehaviour
{
    private Timer timer;
    private float interval = 2f;
    private int bulletRound = 10;
    private bool isActive;

    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.TargetTime = interval;
    }

    public void Initiate()
    {
        if (isActive)
            bulletRound = 10;
        else
        {
            isActive = true;
            timer.ScheduleTask(() => ShootBullets());
        }
    }

    private void ShootBullets()
    {
        bulletRound--;
        int count = 10;
        float angleGap = 360.0f / count;
        float angle = 0;

        for (int i = 0; i <= count; i++)
        {
            GameObject bullet = GameManager.GetBullet();
            bullet.transform.position = transform.position;

            Bullet b = bullet.GetComponent<Bullet>();
            b.Direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            b.Launch();
            angle += angleGap;
        }

        if (bulletRound == 0)
        {
            isActive = false;
            gameObject.SetActive(false);
            return;
        }

        timer.ScheduleTask(() => ShootBullets());
    }
}