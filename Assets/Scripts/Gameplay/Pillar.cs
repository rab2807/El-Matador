using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : CarriableCollision
{
    protected override void CollisionActivity(Collision2D other)
    {
        Villain villain = other.gameObject.GetComponent<Villain>();
        Player player = other.gameObject.GetComponent<Player>();

        if (villain != null)
            GameManager.ReturnPillar(gameObject);
        else if (player != null)
        {
            if (player.IsPushed)
            {
                player.IsPushed = false;
                player.GetComponent<Rigidbody2D>().velocity *= 0.1f;
                GameManager.ReturnPillar(gameObject);
            }
        }
    }
}