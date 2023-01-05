using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Serialization;

public class Bomb : MonoBehaviour
{
    private GameObject villain;
    private GameObject target;

    public float speed = 10f;
    public float launchHeight = 2;

    private Vector3 movePosition;
    private Vector3 targetPosition;

    private float playerX;
    private float targetX;
    private float nextX;
    private float dist;
    private float baseY;
    private float height;
    
    public void Initiate(GameObject villain, GameObject target)
    {
        this.villain = villain;
        targetPosition = target.transform.position;
        transform.position = villain.transform.position;
        
    }

    void Update()
    {
        playerX = villain.transform.position.x;
        targetX = targetPosition.x;
        dist = targetX - playerX;
        nextX = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);
        baseY = Mathf.Lerp(villain.transform.position.y, targetPosition.y, (nextX - playerX) / dist);
        height = launchHeight * (nextX - playerX) * (nextX - targetX) / (-0.25f * dist * dist);

        movePosition = new Vector3(nextX, baseY + height, transform.position.z);

        transform.rotation = LookAtTarget(movePosition - transform.position);
        transform.position = movePosition;

        if (Vector3.Distance(movePosition, targetPosition) < 0.1)
        {
            Explode();
            GameManager.ReturnBomb(gameObject);
        }
    }

     static Quaternion LookAtTarget(Vector2 r)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(r.y, r.x) * Mathf.Rad2Deg);
    }

    void Explode()
    {
        // damage player and villain
        print("exploded");
    }
}