using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainsawThrower : MonoBehaviour
{
    private Timer timer;
    private float interval = 5f;
    private int count = 3;
    private bool isActive;

    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.TargetTime = interval;
    }

    public void Initiate()
    {
        if (isActive)
            count = 3;
        else
        {
            isActive = true;
            timer.ScheduleTask(Random.Range(3.0f, 5.0f), () => PlaceChainsaw());
        }
    }

    private void PlaceChainsaw()
    {
        count--;
        GameObject chainsaw = GameManager.GetChainsaw();
        chainsaw.GetComponent<Chainsaw>().Initiate();
        chainsaw.transform.position = transform.position;

        if (count == 0)
        {
            isActive = false;
            gameObject.SetActive(false);
            return;
        }

        timer.ScheduleTask(Random.Range(3.0f, 5.0f), () => PlaceChainsaw());
    }

    void Update()
    {
    }
}