using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villain : MonoBehaviour
{
    private float walkingSpeed = 1.5f;
    private float chargingSpeed = 5f;
    private bool inChargingPhase;
    private float forceMagnitude = 200;

    private bool isPaused;
    private Timer pauseTimer;
    private float pauseTime = 0.8f;

    private Vector3 direction;
    private GameObject player;

    private bool gunActivated;
    private bool bombActivated;
    private bool chainsawActivated;
    private Timer powerupTimer;
    private float powerupTime;

    public bool InChargingPhase
    {
        get => inChargingPhase;
        set => inChargingPhase = value;
    }

    void Start()
    {
        pauseTimer = gameObject.AddComponent<Timer>();
        pauseTimer.TargetTime = pauseTime;

        powerupTimer = gameObject.AddComponent<Timer>();
        powerupTimer.TargetTime = powerupTime;

        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<CircleCollider2D>().radius =
            GetComponent<SpriteRenderer>().bounds.size.x / 2; // set collider radius from sprite size

        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isPaused && player)
        {
            direction = player.transform.position - transform.position;
            direction = direction.normalized;
            float speed = InChargingPhase ? chargingSpeed : walkingSpeed;

            Vector3 position = transform.position;
            position.x += direction.x * speed * Time.deltaTime;
            position.y += direction.y * speed * Time.deltaTime;
            transform.position = position;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Player>() != null)
        {
            Player p = col.gameObject.GetComponent<Player>();
            Rigidbody2D rb2d = col.gameObject.GetComponent<Rigidbody2D>();

            if (!p.IsPushed) // player in normal state and can be pushed
            {
                p.pushPlayer();
                rb2d.velocity = Vector3.zero;
                Vector3 force = direction * forceMagnitude;
                rb2d.AddForce(force, ForceMode2D.Impulse);
                
                isPaused = true;
                pauseTimer.ScheduleTask(() => isPaused = false);
            }
            else // player in pushed state, can attack the villain
            {
                // player smack cooldown
                isPaused = true;
                pauseTimer.ScheduleTask(() => isPaused = false);

                // decrease life
            }
        }
        else if (col.gameObject.GetComponent<Pillar>() != null)
        {
            if (inChargingPhase)
            {
                // pillar smack cooldown
                isPaused = true;
                pauseTimer.ScheduleTask(() => isPaused = false);

                // decrease life
            }
        }
    }

    public void ActivatePowerUp(string name)
    {
        if (name == "gun")
        {
            GameObject obj = transform.GetChild(1).gameObject;
            obj.SetActive(true);
            obj.GetComponent<BulletThrower>().Initiate();
        }
        else if (name == "bomb")
        {
            GameObject obj = transform.GetChild(2).gameObject;
            obj.SetActive(true);
            obj.GetComponent<BombThrower>().Initiate();
        }
        else if (name == "chainsaw")
        {
            GameObject obj = transform.GetChild(3).gameObject;
            obj.SetActive(true);
            obj.GetComponent<ChainsawThrower>().Initiate();
        }
    }

    void ThrowBomb()
    {
    }

    void ThrowChainsaw()
    {
    }
}