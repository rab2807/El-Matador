using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupGenerator : MonoBehaviour
{
    private Timer timer;
    private int maxObjectNum = 3;
    private float minSpawnTime = 5;
    private float maxSpawnTime = 8;

    private int gunNum;
    private int bombNum;
    private int chainsawNum;
    private float radius;

    private void Start()
    {
        timer = gameObject.AddComponent<Timer>();

        // obtain collider radius
        GameObject obj = GameManager.GetPillar();
        radius = obj.GetComponent<CircleCollider2D>().radius;
        GameManager.ReturnPillar(obj);

        timer.ScheduleTask(Random.Range(minSpawnTime, maxSpawnTime), Generate);
    }

    private void Generate()
    {
        gunNum = GameObject.FindGameObjectsWithTag("gun").Length;
        bombNum = GameObject.FindGameObjectsWithTag("bomb").Length;
        chainsawNum = GameObject.FindGameObjectsWithTag("chainsaw").Length;

        if (gunNum + bombNum + chainsawNum < maxObjectNum) SpawnObject();
        timer.ScheduleTask(Random.Range(minSpawnTime, maxSpawnTime), Generate);
    }

    void SpawnObject()
    {
        var obj = GameManager.GetPowerUp();

        Vector3 position;
        do
        {
            position = obj.transform.position;
            position.x = Random.Range(ScreenData.Left + radius * 4, ScreenData.Right - radius * 4);
            position.y = Random.Range(ScreenData.Top - radius * 4, ScreenData.Bottom + radius * 4);
            obj.transform.position = position;
        } while (Physics2D.OverlapCircle(position, obj.GetComponent<CircleCollider2D>().radius * 1.5f));
    }
}