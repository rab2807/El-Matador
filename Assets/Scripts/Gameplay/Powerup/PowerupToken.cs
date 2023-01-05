using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupToken : MonoBehaviour
{
    private SpriteRenderer s;
    private float fadeSpeed = 0.002f;

    void Start()
    {
        s = GetComponent<SpriteRenderer>();
        CircleCollider2D col = GetComponent<CircleCollider2D>();
        col.radius = GetComponent<SpriteRenderer>().bounds.size.x / 2; // set collider radius from sprite size
    }

    private void Update()
    {
        Color c = s.color;
        float x = c.a;
        x -= fadeSpeed;
        s.color = new Color(c.r, c.g, c.b, x);

        if (x < 0.01)
            GameManager.ReturnPowerUp(gameObject);
        transform.Rotate(Vector3.back, 5f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Player p = col.gameObject.GetComponent<Player>();
        if (p != null)
        {
            // activate powerup

            GameManager.ReturnPowerUp(gameObject);
        }
    }
}