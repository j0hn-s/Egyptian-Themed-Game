using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask whatIsSolid;
    [SerializeField] private int damage;
    [SerializeField] private GameObject destoryEffect;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestoryProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null) {
            if (hitInfo.collider.CompareTag("Enemy")) {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            DestoryProjectile();
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }


    void DestoryProjectile(){
        Instantiate(destoryEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
