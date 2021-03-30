using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting_monster : Enemy {
    private float fireRate;
    private float nextFire;

    public Transform wayPoint1, wayPoint2;
    private Transform wayPointTarget;

    private void Awake() {
        wayPointTarget = wayPoint1;//at the beginning, the bat move to the right way point.
        fireRate = 1f;
        nextFire = Time.time;
    }

    protected override void Attack() {
        if (PlayerController.Instance != null && PlayerController.Instance.gameObject != null){
            base.Attack();

            if (Time.time > nextFire && Vector2.Distance(transform.position, target.position) < distance) {
                Instantiate(Bullet, transform.position, Quaternion.identity);
                nextFire = Time.time + fireRate;
            }
        }
    }

    protected override void Introduction() {
        //base.Introduction();
    }
    protected override void FollowPlayer() {
        base.FollowPlayer();
        if (Vector2.Distance(transform.position, target.position) > distance) {   //When we reached at the wayPoint1, we have to move to the waypoint2
            if (Vector2.Distance(transform.position, wayPoint1.position) < 0.01f) {
                wayPointTarget = wayPoint2;
                Vector3 localTemp = transform.localScale;
                localTemp.x *= -1;
                transform.localScale = localTemp;
            }
            if (Vector2.Distance(transform.position, wayPoint2.position) < 0.01f) {
                wayPointTarget = wayPoint1;
                Vector3 localTemp = transform.localScale;
                localTemp.x *= -1;
                transform.localScale = localTemp;
            }
            transform.position = Vector2.MoveTowards(transform.position,wayPointTarget.position,moveSpeed * Time.deltaTime);
        }
    }
    protected override void TurnDirection() {
        // base.TurnDirection();
    }


}
