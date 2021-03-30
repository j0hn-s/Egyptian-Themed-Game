﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_statue : Enemy
{
    public float x;
    public float y;
    public GameObject winning;
    

    

    private void Awake()
    {

    }

    protected override void Introduction()
    {
        //base.Introduction();
    }

    protected override void TurnDirection()
    {
        // base.TurnDirection();
    }

    protected override void Attack()
    {
        //base.Attack();
    }

    protected override void Death()
    {
        Instantiate(Coin, transform.position + new Vector3(y,x), Quaternion.identity);
        if (gameObject != null)
        {
            winning.SetActive(true);
            Destroy(gameObject);
        }
    }

}
