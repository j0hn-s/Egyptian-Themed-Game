using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Enemy {

    public Transform wayPoint1, wayPoint2;
    private Transform wayPointTarget;

    private void Awake() {
        wayPointTarget = wayPoint1;//at the beginning, the bat move to the right way point.

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

            transform.position = Vector2.MoveTowards(transform.position, wayPointTarget.position, moveSpeed * Time.deltaTime);

        }
    }
    protected override void TurnDirection() {
        // base.TurnDirection();
    }

    protected override void Attack() {
        //base.Attack();
    }
    
}
