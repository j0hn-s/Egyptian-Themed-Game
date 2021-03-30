using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    [SerializeField] private Triggers colliderTrigger;
   

    void Start()
    {
        colliderTrigger.OnPlayerEnterTrigger += Triggers_OnPlayerEnterTrigger;
    }

    private void Triggers_OnPlayerEnterTrigger(object sender, System.EventArgs e)
    {
        StartBattle();
        colliderTrigger.OnPlayerEnterTrigger -= Triggers_OnPlayerEnterTrigger;
    
    }

    private void StartBattle()
    {
        Debug.Log("StartBattle");
    
    
    }

    
}
