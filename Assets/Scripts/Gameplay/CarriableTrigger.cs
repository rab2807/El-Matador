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
            
            AudioManager.Play("crush");
            string objectName = gameObject.tag;

            SpriteRenderer spriteRenderer = transform.parent.gameObject.GetComponent<SpriteRenderer>();
            var color = spriteRenderer.color;
            color = new Color(color.r, color.g, color.b, 1);
            spriteRenderer.color = color;

            transform.parent.gameObject.SetActive(false);
            p.ToggleIsCarrying(objectName);
        }
    }
}
