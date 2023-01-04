using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : CarriableCollision
{
    protected override void CollisionActivity(Collision2D other)
    {
        Villain villain = other.gameObject.GetComponent<Villain>();
        Player player = other.gameObject.GetComponent<Player>();

        if (villain != null)
            GameManager.ReturnMirror(gameObject);
        if (player != null)
        {
            // 
        }
    }
}
