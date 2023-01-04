using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villain : MonoBehaviour
{
    private float walkingSpeed = 1f;
    private float chargingSpeed = 5f;
    private bool inWalkingPhase;
    private bool inChargingPhase;
    private float forceMagnitude = 35f;

    private bool isPaused;
    private Timer pauseTimer;
    private float pauseTime = 0.8f;

    private Vector3 direction;
    private GameObject player;

    public bool InWalkingPhase => inWalkingPhase;

    public bool InChargingPhase
    {
        get => inChargingPhase;
        set => inChargingPhase = value;
    }

    void Start()
    {
        pauseTimer = gameObject.AddComponent<Timer>();
        pauseTimer.TargetTime = pauseTime;

        player = GameObject.FindGameObjectWithTag("Player");
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        collider.radius = GetComponent<SpriteRenderer>().bounds.size.x / 2; // set collider radius from sprite size
    }

    void Update()
    {
        if (!isPaused)
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

            if (!p.IsPushed) // player in normal state and can be pushed
            {
                p.pushPlayer();
                Rigidbody2D rb2d = col.gameObject.GetComponent<Rigidbody2D>();
                rb2d.velocity = Vector3.zero;
                Vector3 force = direction * forceMagnitude;
                rb2d.AddForce(force, ForceMode2D.Impulse);
            }
            else // player in pushed state, can attack the villain
            {
                // player stops after the attack
                p.IsPushed = false;
                Rigidbody2D rb2d = col.gameObject.GetComponent<Rigidbody2D>();
                rb2d.velocity *= 0.1f;

                // player smack cooldown
                isPaused = true;
                pauseTimer.ScheduleTask(() => isPaused = false);

                // decrease life
            }

            // push cooldown
            isPaused = true;
            pauseTimer.ScheduleTask(() => isPaused = false);
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
}