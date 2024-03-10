using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlaform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Jugador")
        {
            other.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Jugador")
        {
            other.gameObject.transform.SetParent(null);
        }
    }
}
