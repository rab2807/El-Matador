using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class Villain : MonoBehaviour
{
    private float walkingSpeed = 1.5f;
    private float chargingSpeed = 3.2f;
    private bool inChargingPhase;
    private float forceMagnitude = 40;

    private bool isPaused;
    private Timer pauseTimer;
    private float pauseTime = 1.0f;

    private Vector3 direction;
    private GameObject player;

    private bool gunActivated;
    private bool bombActivated;
    private bool chainsawActivated;
    private Timer powerupTimer;
    private float powerupTime;

    private VillainAnimation animation;

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

        animation = GetComponent<VillainAnimation>();
    }

    void Update()
    {
        if (!isPaused && player)
        {
            direction = player.transform.position - transform.position;
            direction = direction.normalized;
            float speed = InChargingPhase ? chargingSpeed : walkingSpeed;

            // animation state
            if (!inChargingPhase && !HasPowerUp())
            {
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    animation.ChangeState(direction.x > 0 ? "right" : "left");
                }
                else
                {
                    animation.ChangeState(direction.y > 0 ? "rear" : "front");
                }
            }
            else if (inChargingPhase && !HasPowerUp())
            {
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    animation.ChangeState(direction.x > 0 ? "rightAngry" : "leftAngry");
                }
                else
                {
                    animation.ChangeState(direction.y > 0 ? "rearAngry" : "frontAngry");
                }
            }
            else if (HasPowerUp())
            {
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    animation.ChangeState(direction.x > 0 ? "rightBomb" : "leftBomb");
                }
                else
                {
                    animation.ChangeState(direction.y > 0 ? "rearBomb" : "frontBomb");
                }
            }

            if (isPaused)
                animation.ChangeState("leftCollision");

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
                print("player in normal state. Bull pushed");

                p.PushPlayer();
                rb2d.velocity = Vector3.zero;
                Vector3 force = direction * forceMagnitude;
                rb2d.AddForce(force, ForceMode2D.Impulse);

                PauseVillain(false);
            }
            else // player in pushed state, can attack the villain
            {
                print("player in pushed state. Bull damaged by player");
                AudioManager.Play("dizzy");

                PauseVillain(true);
                ScoreManager.DecreaseLifeVillain("mirror");
            }
        }
        else
        {
            if (col.gameObject.GetComponent<Pillar>() != null)
            {
                if (inChargingPhase)
                {
                    print("bull in charging state. Bull damaged by pillar");
                    AudioManager.Play("dizzy");

                    PauseVillain(true);
                    ScoreManager.DecreaseLifeVillain("pillar");
                }
            }
            else if (col.gameObject.GetComponent<Bullet>() != null)
            {
                print("Bull damaged by bullet");
                AudioManager.Play("dizzy");

                PauseVillain(true);
                ScoreManager.DecreaseLifeVillain("bullet");
            }
            else if (col.gameObject.GetComponent<Bomb>() != null)
            {
                print("Bull damaged by bullet");
                AudioManager.Play("dizzy");

                PauseVillain(true);
                ScoreManager.DecreaseLifeVillain("bomb");
            }
            else if (col.gameObject.GetComponent<Chainsaw>() != null)
            {
                print("Bull damaged by bullet");
                AudioManager.Play("dizzy");

                if (col.gameObject.GetComponent<Chainsaw>())
                    isPaused = true;
                pauseTimer.ScheduleTask(() => isPaused = false);

                // decrease life
                ScoreManager.DecreaseLifeVillain("bullet");
            }
        }
    }

    public void ActivatePowerUp(string name)
    {
        AudioManager.Play("powerup");

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

    public void PauseVillain(bool damage)
    {
        isPaused = true;
        if (damage)
            animation.ChangeState(direction.x >= 0 ? "rightCollision" : "leftCollision");
        pauseTimer.ScheduleTask(() => isPaused = false);
    }

    private bool HasPowerUp()
    {
        return transform.GetChild(1).gameObject.activeSelf ||
               transform.GetChild(2).gameObject.activeSelf ||
               transform.GetChild(3).gameObject.activeSelf;
    }
}