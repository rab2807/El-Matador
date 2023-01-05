using System;
using System.Collections;
using System.Collections.Generic;
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
                // decrease life
            }
            else if (col.gameObject.GetComponent<Mirror>() != null)
            {
                // decrease life
            }

            GameManager.ReturnBullet(gameObject);
        }
    }
}