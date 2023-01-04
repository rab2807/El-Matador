using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : CarriableCollision
{
    protected override void CollisionActivity(Collision2D other)
    {
        Villain villain = other.gameObject.GetComponent<Villain>();
        if (villain != null)
        {
            if(villain.InWalkingPhase)
                GameManager.ReturnPillar(gameObject);
            if (villain.InChargingPhase)
            {
                // pillar e bari kheye health kombe
            }
        }
    }
}
