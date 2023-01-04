using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriableTrigger : MonoBehaviour
{
    void Start()
    {
        float r = transform.parent.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        GetComponent<CircleCollider2D>().radius = r;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        float input3 = Input.GetAxis("Fire1");
        Player p = col.gameObject.GetComponent<Player>();
        if (input3 != 0 && p != null && p.PickupFlag)
        {
            print("picked up!");
            string objectName = gameObject.tag;
            transform.parent.gameObject.SetActive(false);
            p.toggleIsCarrying(objectName);
        }
    }

    void Update()
    {
        
    }
}
