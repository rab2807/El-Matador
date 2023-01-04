using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriableGenerator : MonoBehaviour
{
    private Timer timer;
    private int maxObjectNum = 6;
    private float minSpawnTime = 2;
    private float maxSpawnTime = 6;

    private int currentPillarNum;
    private int currentMirrorNum;
    private float radius;

    private void Start()
    {
        timer = gameObject.AddComponent<Timer>();

        // obtain collider radius
        GameObject obj = GameManager.GetPillar();
        radius = obj.GetComponent<CircleCollider2D>().radius;
        GameManager.ReturnPillar(obj);

        Generate();
    }

    private void Generate()
    {
        currentPillarNum = GameObject.FindGameObjectsWithTag("pillar").Length;
        currentMirrorNum = GameObject.FindGameObjectsWithTag("mirror").Length;
        if (currentPillarNum + currentMirrorNum < maxObjectNum)
            SpawnObject();
        timer.ScheduleTask(Random.Range(minSpawnTime, maxSpawnTime), Generate);
    }

    void SpawnObject()
    {
        float x = Random.Range(0, 50);
        print(x);
        GameObject obj;
        if (x < 25)
            obj = GameManager.GetPillar();
        else
            obj = GameManager.GetMirror();
        
        Vector3 position = obj.transform.position;
        position.x = Random.Range(ScreenData.Left + radius * 4, ScreenData.Right - radius * 4);
        position.y = Random.Range(ScreenData.Top - radius * 4, ScreenData.Bottom + radius * 4);
        obj.transform.position = position;
    }
}