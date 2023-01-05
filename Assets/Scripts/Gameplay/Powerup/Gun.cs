using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Timer timer;
    private float interval = 2f;

    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.TargetTime = interval;
        timer.ScheduleTask(() => ShootBullets());
    }

    private void ShootBullets()
    {
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
        
        timer.ScheduleTask(() => ShootBullets());
    }
}