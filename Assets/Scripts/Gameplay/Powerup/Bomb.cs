using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Manager;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float explodeRadius;
    private GameObject villain;
    private GameObject target;

    public float speed = 13f;
    public float launchHeight = 2;

    private Vector3 movePosition;
    private Vector3 targetPosition;
    private Vector3 sourcePosition;

    private float playerX;
    private float targetX;
    private float nextX;
    private float dist;
    private float baseY;
    private float height;

    private GameObject tracker;

    public void Initiate(Vector3 target)
    {
        tracker = GameObject.FindGameObjectWithTag("tracker");
        explodeRadius = tracker.GetComponent<SpriteRenderer>().bounds.size.x / 1.5f;

        targetPosition = target;

        float x1 = target.x - ScreenData.Left;
        float x2 = ScreenData.Right - target.x;

        playerX = x1 > x2 ? ScreenData.Left - 0.5f : ScreenData.Right + 0.5f;
        transform.position = new Vector2(playerX, ScreenData.Bottom + (ScreenData.Top - ScreenData.Bottom) / 2.0f);
        sourcePosition = transform.position;
    }

    void Update()
    {
        // playerX = villain.transform.position.x;
        targetX = targetPosition.x;
        dist = targetX - playerX;
        nextX = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);
        baseY = Mathf.Lerp(sourcePosition.y, targetPosition.y, (nextX - playerX) / dist);
        height = launchHeight * (nextX - playerX) * (nextX - targetX) / (-0.25f * dist * dist);

        movePosition = new Vector3(nextX, baseY + height, transform.position.z);

        transform.rotation = LookAtTarget(movePosition - transform.position);
        transform.position = movePosition;

        if (Vector3.Distance(movePosition, targetPosition) < 0.1)
        {
            Explode();
            tracker.SetActive(false);
            GameManager.ReturnBomb(gameObject);
        }
    }

    static Quaternion LookAtTarget(Vector2 r)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(r.y, r.x) * Mathf.Rad2Deg);
    }

    void Explode()
    {
        AudioManager.Play("explode");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explodeRadius);
        foreach (var col in colliders)
        {
            if (col.GetComponent<Player>())
            {
                print("Player damaged by bomb");
                AudioManager.Play("ouch");
                ScoreManager.DecreaseLifePlayer("bomb");
            }
            else if (col.GetComponent<Villain>())
            {
                Villain villain = col.GetComponent<Villain>();
                print("Bull damaged by bomb");
                AudioManager.Play("dizzy");

                villain.PauseVillain(true);
                ScoreManager.DecreaseLifeVillain("bomb");
            }
            else if (col.GetComponent<Mirror>())
            {
                GameManager.ReturnMirror(col.gameObject);
            }
            else if (col.GetComponent<Pillar>())
            {
                GameManager.ReturnPillar(col.gameObject);
            }
        }

        print("exploded");
    }
}