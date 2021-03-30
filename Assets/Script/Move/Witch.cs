using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Enemy {
    private float moveRate = 2.0f;
    private float moveTimer;
    private float shootRate = 2.0f;
    private float shootTimer;

    private Animator amim;
    public GameObject projectile;


    [SerializeField] private float minX, maxX, minY, maxY;


    private void Start() {
        amim = GetComponent<Animator>();
    }
    protected override void Introduction() {
        //base.Introduction();
    }
    protected override void TurnDirection() {
        base.TurnDirection();
    }
    protected override void FollowPlayer() {
        //base.FollowPlayer();
        RandomMove();
    }
    private void RandomMove() {
        moveTimer += Time.deltaTime;
        if (moveTimer > moveRate) {
            transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            moveTimer = 0;
        }
        TurnDirection();

    }

    protected override void Attack() {
        base.Attack();
        shootTimer += Time.deltaTime;
        if (shootTimer > shootRate) {
            Instantiate(projectile, transform.position, Quaternion.identity);
            shootTimer = 0;
        }
    }














}
