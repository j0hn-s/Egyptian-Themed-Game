using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] protected private float moveSpeed;
    [SerializeField] private string enemyName;
    [SerializeField] private float maxHP;
    public float hp;
    public int damage;
    public float FlashTime;
    private SpriteRenderer sr;
    private Color originColor;
    [SerializeField] protected private float distance;
    public GameObject Coin;
    public GameObject Bullet;
    private SpriteRenderer sp;

    public Transform target; //The target is our player;

    private PlayerController playerhealth;

    // Start is called before the first frame update
    void Start() {
        Introduction();
        hp = maxHP;
        target = PlayerController.Instance.GetComponent<Transform>();
        sp = GetComponent<SpriteRenderer>();
        sr = GetComponent<SpriteRenderer>();
        originColor = sr.color;
        playerhealth = PlayerController.Instance;
    }

    // Update is called once per frame
    void Update() {

        TurnDirection();

        if (hp <= 0) {
            Death();
        }
        Attack();
        // if(Input.GetKeyDown(KeyCode.J))
        // {
        //     TakeDamage(damage);
        // }

        // DisplayHpBar();
    }

    private void FixedUpdate() {
        if (PlayerController.Instance != null){
            FollowPlayer();
        }
    }
    protected virtual void FollowPlayer() {
        if (Vector2.Distance(transform.position, target.position) < distance) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }
    protected virtual void Introduction() {
        Debug.Log("My name is " + enemyName + ",HP: " + hp + ",Movespeed: " + moveSpeed);
    }
    //turndirection of enemy
    protected virtual void TurnDirection() {
        if (transform.position.x > target.position.x) {
            sp.flipX = false;
        } else {
            sp.flipX = true;
        }
    }

    protected virtual void Attack() {
        // Debug.Log(enemyName + "is attacking!");
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (playerhealth != null) {
                playerhealth.DamagePlayer(damage);
            }
        }
    }
    public void TakeDamage(int damage) {
        GetComponentInChildren<HealthBar>().hp -= damage;
        hp = GetComponentInChildren<HealthBar>().hp;
        if (hp != maxHP) { FlashColor(FlashTime); }
    }

    protected virtual void Death() {
        if (this != null){
            Instantiate(Coin, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    
    //     protected virtual void DisplayHpBar()
    //     {
    //         hpImage.fillAmount = hp/maxHP;

    //     }

    public void FlashColor(float time) {
        sr.color = Color.red;
        Invoke("ResetColor", time);
    }
    public void ResetColor() {
        sr.color = originColor;
    }
}
