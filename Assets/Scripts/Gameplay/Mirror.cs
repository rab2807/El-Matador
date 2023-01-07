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
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
            GameManager.ReturnMirror(gameObject);
        }

        if (player != null)
        {
            if (player.IsPushed)
            {
                AudioManager.Play("bounce");

                Rigidbody2D rb2d = player.GetComponent<Rigidbody2D>();
                float forceMagnitude = rb2d.velocity.magnitude;
                rb2d.velocity = Vector3.zero;
                Vector3 force = (GameObject.FindGameObjectWithTag("villain").transform.position - transform.position)
                                .normalized *
                                forceMagnitude;
                rb2d.AddForce(force, ForceMode2D.Impulse);
                
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
                GameManager.ReturnMirror(gameObject);
            }
        }
    }
}