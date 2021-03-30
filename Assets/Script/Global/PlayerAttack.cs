using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public int damage;
    public float disabletime;
    public float startTime;
    private Animator anim;
    private PolygonCollider2D coll2d;

    // Start is called before the first frame update
    void Start() {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        coll2d = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        Attack();
    }
    void Attack() {
        if (Input.GetKeyDown(KeyCode.J)) {
            anim.SetBool("idle", false);
            anim.SetBool("isAttack", true);
            // StartCoroutine(startHitAnimation());
            // StartCoroutine(startHitbox());
        } else {
            anim.SetBool("isAttack", false);
        }
    }

    IEnumerator startHitbox() {
        yield return new WaitForSeconds(startTime);
        coll2d.enabled = true;
        StartCoroutine(disableHitbox());
    }
    IEnumerator disableHitbox() {
        yield return new WaitForSeconds(disabletime);
        coll2d.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }



}
