using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 10f;
    private string isCarrying = "none"; // the player carrying any pickup or not
    private float pickupTime = 0.5f; // just after picking up an object, player have to wait some time to place it down
    private bool pickupFlag = true; // boolean flag to check the above task
    private Timer pickupTimer;

    public string IsCarrying
    {
        get => isCarrying;
        set => isCarrying = value;
    }

    public bool PickupFlag => pickupFlag;

    void Start()
    {
        CircleCollider2D collider2D = GetComponent<CircleCollider2D>();
        collider2D.radius = GetComponent<SpriteRenderer>().bounds.size.x / 2; // set collider radius from sprite size
        pickupTimer = gameObject.AddComponent<Timer>();
        pickupTimer.TargetTime = pickupTime;
    }

    private bool frameFlag; // prevents multiple inputs taken in a single frame

    void Update()
    {
        Vector3 position = transform.position;

        float input1 = Input.GetAxis("Horizontal");
        float input2 = Input.GetAxis("Vertical");
        float input3 = Input.GetAxis("Fire1");

        if (!frameFlag)
        {
            frameFlag = true;
            if (input1 != 0)
            {
                position.x += input1 * speed * Time.deltaTime;
            }

            if (input2 != 0)
            {
                position.y += input2 * speed * Time.deltaTime;
            }

            if (input3 > 0 && isCarrying != "none" && pickupFlag)
            {
                print("placed it here!");
                
                GameObject obj = null;
                if (isCarrying == "pillar")
                    obj = GameManager.GetPillar();
                else if (isCarrying == "mirror")
                    obj = GameManager.GetMirror();
                obj.transform.position = transform.position;
                
                toggleIsCarrying("none");
            }
        }
        else frameFlag = false;

        transform.position = position;
    }

    // 
    public void toggleIsCarrying(string objectName)
    {
        isCarrying = objectName;
        pickupFlag = !pickupFlag;
        pickupTimer.ScheduleTask(() => { pickupFlag = !pickupFlag; });
    }
}