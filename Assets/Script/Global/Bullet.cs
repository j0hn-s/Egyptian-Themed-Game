using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Animator animator;

    float moveSpeed = 5f;
    Rigidbody2D rb;

    PlayerController target;
    Vector2 moveDirection;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        target = PlayerController.Instance;
        if (target){
            moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
            Destroy(gameObject, 3f);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name.Equals("Player")) {
            animator.SetBool("Death", true);
            Destroy(gameObject, 0.5f);
        }
    }
}
