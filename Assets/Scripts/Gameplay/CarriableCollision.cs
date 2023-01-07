using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarriableCollision : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    private float fadeSpeed = 0.001f;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        float r = spriteRenderer.bounds.size.x / 2;
        GetComponent<CircleCollider2D>().radius = r;
    }

    private void Update()
    {
        Color c = spriteRenderer.color;
        float x = c.a;
        x -= fadeSpeed;
        spriteRenderer.color = new Color(c.r, c.g, c.b, x);

        if (x < 0.1)
        {
            spriteRenderer.color = new Color(c.r, c.g, c.b, 1);
            if (GetComponent<Pillar>())
                GameManager.ReturnPillar(gameObject);
            else
                GameManager.ReturnMirror(gameObject);
        }
    }

    protected abstract void CollisionActivity(Collision2D other);

    private void OnCollisionEnter2D(Collision2D other)
    {
        CollisionActivity(other);
    }
}