﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillainTrigger : MonoBehaviour
{
    private Villain villain;
     void Start()
     {
         villain = transform.parent.gameObject.GetComponent<Villain>();
     }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            villain.InChargingPhase = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            villain.InChargingPhase = false;
        }
    }
}
