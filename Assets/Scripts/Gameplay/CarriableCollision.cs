using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarriableCollision : MonoBehaviour
{
    void Start()
    {
        float r = gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        GetComponent<CircleCollider2D>().radius = r;
    }

    // implement accordingly
    protected abstract void CollisionActivity(Collision2D other);
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        CollisionActivity(other);
    }
}
