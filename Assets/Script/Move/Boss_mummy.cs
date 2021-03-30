using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_mummy : Enemy
{
    public float fireRate;
    public float fireRate2;
    private float nextFire;
    private float nextFire2;
    public GameObject Bat;
    public GameObject winpanel;
    

    public Transform wayPoint1, wayPoint2;
    private Transform wayPointTarget;


    private void Awake()
    {
        wayPoint1 = GameObject.Find("BossLeftPoint").transform;
        wayPoint2 = GameObject.Find("BossRightPoint").transform;
        wayPointTarget = wayPoint1;//at the beginning, the bat move to the right way point.
        nextFire = Time.time;
        nextFire2 = Time.time;
    }

    protected override void Attack()
    {

        base.Attack();

        if (Time.time > nextFire && Vector2.Distance(transform.position, target.position) < distance)
        {
            Instantiate(Bullet, transform.position, Quaternion.identity);
            Instantiate(Bullet, transform.position + new Vector3(1,0), Quaternion.identity);
            Instantiate(Bullet, transform.position + new Vector3(0,1), Quaternion.identity);

            

            nextFire = Time.time + fireRate;
        }
        if (Time.time > nextFire2 && Vector2.Distance(transform.position, target.position) < distance)
        {

            Instantiate(Bat, transform.position, Quaternion.identity);

            nextFire2 = Time.time + fireRate2;
            

        }
    }

    protected override void Introduction()
    {
        //base.Introduction();
    }
    protected override void FollowPlayer()
    {
        base.FollowPlayer();
        if (Vector2.Distance(transform.position, target.position) > distance)
        {   //When we reached at the wayPoint1, we have to move to the waypoint2
            if (Vector2.Distance(transform.position, wayPoint1.position) < 0.01f)
            {
                wayPointTarget = wayPoint2;
                Vector3 localTemp = transform.localScale;
                localTemp.x *= -1;
                transform.localScale = localTemp;
            }
            if (Vector2.Distance(transform.position, wayPoint2.position) < 0.01f)
            {
                wayPointTarget = wayPoint1;
                Vector3 localTemp = transform.localScale;
                localTemp.x *= -1;
                transform.localScale = localTemp;
            }
            transform.position = Vector2.MoveTowards(transform.position, wayPointTarget.position, moveSpeed * Time.deltaTime);

        }
    }
    protected override void TurnDirection()
    {
        // base.TurnDirection();
    }

    protected override void Death()
    {
        if (this != null)
        {
            Instantiate(Coin, transform.position, Quaternion.identity);
            winpanel.SetActive(true);
            Destroy(gameObject);
        }
    }




}
