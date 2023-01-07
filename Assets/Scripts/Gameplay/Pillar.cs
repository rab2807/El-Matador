using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class Pillar : CarriableCollision
{
    protected override void CollisionActivity(Collision2D other)
    {
        Villain villain = other.gameObject.GetComponent<Villain>();
        Player player = other.gameObject.GetComponent<Player>();

        if (villain != null)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
            GameManager.ReturnPillar(gameObject);
        }
        else if (player != null)
        {
            if (player.IsPushed)
            {
                print("player is in pushed state. Damaged by pillar");
                AudioManager.Play("ouch");

                player.IsPushed = false;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
                GameManager.ReturnPillar(gameObject);
                ScoreManager.DecreaseLifePlayer("pillar");
            }
        }
    }
}