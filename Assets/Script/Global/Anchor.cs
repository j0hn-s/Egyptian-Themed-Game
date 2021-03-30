using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoSingleton<Anchor> {

    [SerializeField] private GameObject projectile;

    [SerializeField] private Transform shotPoint;

    private Animator animator;

    // Start is called before the first frame update
    void Start() {
        animator = PlayerController.Instance.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 difference = mousePosition - PlayerController.Instance.transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);

        if (Time.timeScale > 0 && Input.GetMouseButtonDown(0) && !PlayerController.Instance.IsAttacking) {
            
            PlayerController.Instance.FlipPlayer(difference.x);
        
            animator.SetBool("idle", false);
            animator.SetBool("isAttack", true);
            animator.SetBool("isRemote", true);
            
            Instantiate(projectile, shotPoint.position, Quaternion.Euler(0f, 0f, rotZ - 90f));
        } else {
            animator.SetBool("isAttack", false);
            animator.SetBool("isRemote", false);
        }

    }

    
}
