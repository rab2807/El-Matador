using System;
using Manager;
using UnityEngine;

public class Chainsaw : MonoBehaviour
{
    private Timer timer;
    private float interval = 2f;
    private SpriteRenderer sprite;
    private bool isOn;
    private int count = 3;

    void Awake()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.TargetTime = interval;

        sprite = GetComponent<SpriteRenderer>();
        GetComponent<CircleCollider2D>().radius = sprite.bounds.size.x / 2; // set collider radius from sprite size
    }

    public void Initiate()
    {
        sprite.color = Color.white;
        isOn = false;
        timer.ScheduleTask(() => Toggle());
    }

    private float angle;

    private void Update()
    {
        if (isOn)
        {
            transform.Rotate(Vector3.back, 6.0f);

            Vector3 position = transform.position;
            position.y += Mathf.Sin(angle) * 0.005f;
            transform.position = position;
        }

        angle += (Time.deltaTime * 20) % 360.0f;
    }

    private void Toggle()
    {
        isOn = !isOn;
        if(isOn) AudioManager.Play("chainsaw");
        sprite.color = isOn ? new Color(230 / 255f, 100 / 255f, 100 / 255f) : Color.white;
        GetComponent<CircleCollider2D>().isTrigger = !isOn;
        timer.ScheduleTask(() => Toggle());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Toggle();
        if (other.gameObject.GetComponent<Player>() != null)
        {
            AudioManager.Play("ouch");

            other.gameObject.GetComponent<Rigidbody2D>().velocity *= 0.1f;

            ScoreManager.DecreaseLifePlayer("chainsaw");
            count--;
            if (count == 0)
                GameManager.ReturnChainsaw(gameObject);
        }
        else if (other.gameObject.GetComponent<Villain>() != null)
        {
            // print("Bull damaged by chainsaw");
            AudioManager.Play("ouch");

            other.gameObject.GetComponent<Villain>().PauseVillain(true);
            ScoreManager.DecreaseLifeVillain("chainsaw");
            count--;
            if (count == 0)
                GameManager.ReturnChainsaw(gameObject);
        }
    }
}