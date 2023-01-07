using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupToken : MonoBehaviour
{
    private SpriteRenderer s;
    private float fadeSpeed = 0.002f;
    private Villain villain;

    void Start()
    {
        s = GetComponent<SpriteRenderer>();
        CircleCollider2D col = GetComponent<CircleCollider2D>();
        col.radius = GetComponent<SpriteRenderer>().bounds.size.x / 2; // set collider radius from sprite size
        villain = GameObject.FindGameObjectWithTag("villain").GetComponent<Villain>();
    }

    private float angle;

    private void Update()
    {
        Color c = s.color;
        float x = c.a;
        x -= fadeSpeed;
        s.color = new Color(c.r, c.g, c.b, x);

        Vector3 position = transform.position;
        position.y += Mathf.Sin(angle) * 0.008f;
        transform.position = position;
        angle += (Time.deltaTime * 10) % 360.0f;

        if (x < 0.1)
        {
            s.color = new Color(c.r, c.g, c.b, 1);
            GameManager.ReturnPowerUp(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Player p = col.gameObject.GetComponent<Player>();
        if (p != null)
        {
            villain.ActivatePowerUp(gameObject.tag);
            GameManager.ReturnPowerUp(gameObject);
        }
    }
}