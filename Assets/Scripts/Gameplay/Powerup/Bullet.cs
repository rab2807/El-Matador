using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    private float forceMagnitude = 3f;
    private Rigidbody2D rb2d;

    public Vector2 Direction
    {
        get => direction;
        set => direction = value;
    }

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        GetComponent<CircleCollider2D>().radius =
            GetComponent<SpriteRenderer>().bounds.size.x / 2; // set collider radius from sprite size
    }

    public void Launch()
    {
        rb2d.AddForce(direction.normalized * forceMagnitude, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Mirror>() == null)
        {
            if (col.gameObject.GetComponent<Player>() != null)
            {
                AudioManager.Play("ouch");

                col.gameObject.GetComponent<Rigidbody2D>().velocity *= 0.1f;
                ScoreManager.DecreaseLifePlayer("bullet");
            }

            GameManager.ReturnBullet(gameObject);
        }
    }

    private void Update()
    {
        gameObject.transform.Rotate(Vector3.back, 1);
    }
}