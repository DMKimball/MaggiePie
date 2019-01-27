﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Bounds")
	    return;

        RespawnScript respawn = collision.gameObject.GetComponent<RespawnScript>();
        if (respawn != null)
        {
            respawn.Respawn();
        }
    }
}
