using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Triggers : MonoBehaviour
{
    public event EventHandler OnPlayerEnterTrigger;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController player = collider.GetComponent<PlayerController>();
        if (player != null)
        {
            OnPlayerEnterTrigger.Invoke(this, EventArgs.Empty);
        
        }
    
    }
}
