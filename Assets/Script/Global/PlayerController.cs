/*
 * @Date: 2020-11-20 07:46:39
 * @Author: Zilin Zhang
 * @LastEditors: Zilin Zhang
 * @LastEditTime: 2020-12-10 18:31:08
 * @FilePath: /SE_CW2_Group8_Yellow/Assets/Script/Global/PlayerController.cs
 * @Description: The function controller of player
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.Experimental.U2D.Animation;

public class PlayerController : MonoSingleton<PlayerController> {

    /**
     * @Description: make the constructor private to avoid other place creating the object by new.
     * @version: 1.0.0
     * @Author: Zilin Zhang
     * @Date: 2020-11-20 20:50:38
     * @LastEditors: Zilin Zhang
     * @LastEditTime: Do not edit
     */
    private PlayerController() {
    }

    [SerializeField]
    private GameObject playerGameObject;
    
    [SerializeField]
    private GameObject leftLeg;

    [SerializeField]
    private GameObject rightLeg;

    private Rigidbody2D rb;
    private Animator anim;
    private AnimatorStateInfo animStateInfo;
    // private SpriteRenderer render;

    private Vector2 movement;

    public float speed;

    public AudioClip coinsound;

    public Tilemap unexploredTilemap;

    public int numBlinks;
    public float seconds;

    public GameObject myBag;

    private bool isOpenbag;
    
    [SerializeField]
    private int vision = 3;
    
    public string scenePwd;

    [SerializeField]
    private GameObject failureDialog;

    private bool isAttacking;

    [SerializeField]
    private GameObject playerAttack;
    PolygonCollider2D coll2d;

    /**
     * @Description: the list to store all equipments resolver
     * @version: 1.0.0
     * @Author: Zilin Zhang
     * @Date: 2020-11-27 03:26:11
     * @LastEditors: Zilin Zhang
     * @LastEditTime: 2020-11-27 03:26:11
     */
    public List<SpriteResolver> spriteResolvers = new List<SpriteResolver>();

    public SpriteResolver leftHandResolve;

    public SpriteResolver rightHandResolve;

    public GameObject itemEquipment;

    public GameObject arrowBag;

    public GameObject winning;

    // Just for the test 
    // TODO: delete it after finished using USE button in invetory.
    public bool isEquiped = false;

    /**
     * @Description: the const of equipment levels, make it easier using in other scrip and avoid error
     * @version: 1.0.0
     * @Author: Zilin Zhang
     * @Date: 2020-11-27 03:27:23
     * @LastEditors: Zilin Zhang
     * @LastEditTime: 2020-11-27 03:27:23
     */
    public const string EQUIPMENT_LEVEL_NORMAL = "normal";
    public const string EQUIPMENT_LEVEL_LOW = "lowLevel";
    public const string EQUIPMENT_LEVEL_MIDDLE = "middleLevel";
    public const string EQUIPMENT_LEVEL_HIGH = "highLevel";
    public const string EQUIPMENT_LEFT_OR_RIGHT_ITEM_NONE = "none";
    public const string EQUIPMENT_LEFT_ITEM_WOOD_SWORD = "woodSword";
    public const string EQUIPMENT_LEFT_ITEM_IRON_SWORD = "ironSword";
    public const string EQUIPMENT_LEFT_ITEM_GOLD_SWORD = "goldSword";
    public const string EQUIPMENT_LEFT_ITEM_WOOD_ARROW = "woodArrow";
    public const string EQUIPMENT_RIGHT_ITEM_WOOD_SHIELD = "woodShield";
    public const string EQUIPMENT_RIGHT_ITEM_WOOD_BOW = "woodBow";
    public const string EQUIPMENT_RIGHT_ITEM_IRON_SHIELD = "ironShield";
    public const string EQUIPMENT_RIGHT_ITEM_IRON_BOW = "ironBow";
    public const string EQUIPMENT_RIGHT_ITEM_GOLD_BOW = "goldBow";


    private bool isEquippedRemoteWeapon = false;

    public bool IsEquippedRemoteWeapon{
        get {return isEquippedRemoteWeapon;}
        set {isEquippedRemoteWeapon = value;}
    }

    public GameObject FailureDialog{
        get { return failureDialog;}
        set { failureDialog = value;}
    }

    public bool IsOpenbag{
        get {return isOpenbag;}
        set {isOpenbag = value;}
    }

    public bool IsAttacking {
        get {return isAttacking;}
        set {isAttacking = value;}
    }

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // Debug.Log(anim);
        animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        coll2d = playerAttack.GetComponent<PolygonCollider2D>();
        // render = GetComponent<SpriteRenderer>();
        
        AddResolverIntoList();
    }

    // Update is called once per frame
    void Update() {
        if (!isAttacking){
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(movement.x, movement.y);
            
            if (movement.x > 0) {
                playerGameObject.transform.localScale = new Vector3(1, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z);
                leftLeg.transform.localScale = new Vector3(1, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z);
                rightLeg.transform.localScale = new Vector3(1, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z);
            } else if(movement.x < 0){
                playerGameObject.transform.localScale = new Vector3(-1, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z);
                leftLeg.transform.localScale = new Vector3(-1, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z);
                rightLeg.transform.localScale = new Vector3(-1, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z);
            }
            SwithAnim();
        }
        // AttackAnim();
        // UpdateFog();
        OpenMyBag();
        
        // check the attack pattern
        if (isEquippedRemoteWeapon) {
            Anchor.Instance.gameObject.SetActive(true);
        } else {
            Anchor.Instance.gameObject.SetActive(false);
        }
        
        // Just for the test 
        // TODO: delete it after finished using USE button in invetory.
        // if (Input.GetKeyDown(KeyCode.Q)) {
        //     if (isEquiped) {
        //         ChangeEquipments(EquipmentType.armour, EQUIPMENT_LEVEL_NORMAL);
        //     } else {
        //         ChangeEquipments(EquipmentType.armour, EQUIPMENT_LEVEL_LOW);
        //     }
        //     isEquiped = !isEquiped;
        // }
        
    }

    private void FixedUpdate() {
        if (!isAttacking){
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }

    void SwithAnim() {
        anim.SetFloat("speed", movement.magnitude);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (GetComponentInChildren<HealthBar>().hp <= 0) {
            gameObject.SetActive(false);
            failureDialog.SetActive(true);
        } else {
            if (col.gameObject.tag == "PlantDMG")
            {
                ScoreScript.scoreValue += 1;
                GetComponentInChildren<HealthBar>().hp -= 5;
                // BlinkPlayer(numBlinks, seconds);
            }
            else if (col.gameObject.tag == "coin")
            {
                ScoreScript.scoreValue += 1;
                AudioSource.PlayClipAtPoint(coinsound, transform.position);
                Destroy(col.gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("WaterDMG"))
        {
            speed = 2; 
            if (GetComponentInChildren<HealthBar>().hp <= 0)
            {
                gameObject.SetActive(false);
                failureDialog.SetActive(true);
            }
            else
            {
                GetComponentInChildren<HealthBar>().hp -= 10;
                // BlinkPlayer(numBlinks, seconds);
            }
        }
        else if (col.CompareTag("Bullet"))
        {
            DamagePlayer(5);
        }
    }
    public void DamagePlayer(int damage) {
        if (GetComponentInChildren<HealthBar>().hp <= 0) {
            gameObject.SetActive(false);
            failureDialog.SetActive(true);
        } else {
            GetComponentInChildren<HealthBar>().hp -= damage;
            // BlinkPlayer(numBlinks, seconds);
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        speed = 5; 
    }

    private void UpdateFog() {
        Vector3Int currentPlayerPos = unexploredTilemap.WorldToCell(transform.position);
        for (int i = -vision; i <= vision; i++) {
            for (int j = -vision; j <= vision; j++) {
                unexploredTilemap.SetTile(currentPlayerPos + new Vector3Int(i, j, -1), null);
            }
        }
    }

    void BlinkPlayer(int numBlinks, float seconds) {
        StartCoroutine(DoBlinks(numBlinks, seconds));
    }

    IEnumerator DoBlinks(int numBlinks, float seconds) {
        for (int i = 0; i < numBlinks * 2; i++) {
            // render.enabled = !render.enabled;
            yield return new WaitForSeconds(seconds);
        }
        // render.enabled = true;
    }

    void OpenMyBag() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            isOpenbag = !isOpenbag;
            myBag.SetActive(isOpenbag);
            if (isOpenbag) {
                Time.timeScale = 0f;
            } else {
                Time.timeScale = 1f;
            }
        }
    }


    private void StartAttackEvent(){
        isAttacking = true;
        if (coll2d.enabled) {
            coll2d.enabled = false;
        }
        coll2d.enabled = true;
    }

    private void EndAttackEvent(){
        isAttacking = false;
        coll2d.enabled = false;
        anim.SetBool("idle", true);
    }
    
    public void FlipPlayer(float x){
        if (x > 0) {
            playerGameObject.transform.localScale = new Vector3(1, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z);
            leftLeg.transform.localScale = new Vector3(1, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z);
            rightLeg.transform.localScale = new Vector3(1, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z);
        } else if(x < 0){
            playerGameObject.transform.localScale = new Vector3(-1, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z);
            leftLeg.transform.localScale = new Vector3(-1, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z);
            rightLeg.transform.localScale = new Vector3(-1, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z);
        }
    }

    /**
     * @Description: Add all equipments resolvers into the list
     * @version: 1.0.0
     * @Author: Zilin Zhang
     * @Date: 2020-11-27 03:29:45
     * @LastEditors: Zilin Zhang
     * @LastEditTime: 2020-11-27 03:29:45
     */
    private void AddResolverIntoList(){
        foreach (var resolover in FindObjectsOfType<SpriteResolver>()) {
            spriteResolvers.Add(resolover);
            if (resolover.GetCategory().Equals("LeftItem")) {
                leftHandResolve = resolover;
            }
            if (resolover.GetCategory().Equals("RightItem")) {
                rightHandResolve = resolover;
            }
        }
    }

    public void RestoreHealth() {
        if(GetComponentInChildren<HealthBar>().hp <= 100) {
            GetComponentInChildren<HealthBar>().hp += 100;
        }
    }
    
    /**
     * @Description: The function of changing equipment, 
                    other script can using PlayerController.Instance.ChangeEquipments to call this function.
                    for the first param, developer could using PlayerController.EquopmentType to choose which equipmentType they want to change,
                    for the second param, developer could using PlayerController.EQUIPMENT_LEVEL_XXX to choose which level of equipment they want to change.
     * @version: 1.0.0
     * @Author: Zilin Zhang
     * @Date: 2020-11-27 03:30:14
     * @LastEditors: Zilin Zhang
     * @LastEditTime: 2020-11-27 03:29:45
     */
    public void ChangeEquipments(EquipmentType equipmentType, string equipmentLevel){
        switch (equipmentType) {
            case EquipmentType.armour:
                foreach (var resolver in spriteResolvers) {
                    if (!(resolver.GetCategory().Equals("LeftItem") || resolver.GetCategory().Equals("RightItem"))) {
                        resolver.SetCategoryAndLabel(resolver.GetCategory(), equipmentLevel);
                    }
                }
                if (equipmentLevel.Equals(EQUIPMENT_LEVEL_NORMAL)) {
                    itemEquipment.SetActive(true);
                } else {
                    itemEquipment.SetActive(false);
                }
                break;
            case EquipmentType.leftHand:
                if (rightHandResolve.GetLabel().Equals(EQUIPMENT_RIGHT_ITEM_WOOD_BOW)){
                    rightHandResolve.SetCategoryAndLabel(rightHandResolve.GetCategory(), EQUIPMENT_LEFT_OR_RIGHT_ITEM_NONE);
                    arrowBag.SetActive(false);
                    isEquippedRemoteWeapon = false;
                }
                leftHandResolve.SetCategoryAndLabel(leftHandResolve.GetCategory(), equipmentLevel);
                break;
            case EquipmentType.rightHand:
                // handle some special type
                // can not use other things in left hand when using bow
                if (equipmentLevel.Equals(EQUIPMENT_RIGHT_ITEM_WOOD_BOW) 
                || equipmentLevel.Equals(EQUIPMENT_RIGHT_ITEM_IRON_BOW) 
                || equipmentLevel.Equals(EQUIPMENT_RIGHT_ITEM_GOLD_BOW)) { 
                    leftHandResolve.SetCategoryAndLabel(leftHandResolve.GetCategory(), EQUIPMENT_LEFT_ITEM_WOOD_ARROW);
                    rightHandResolve.SetCategoryAndLabel(rightHandResolve.GetCategory(), EQUIPMENT_RIGHT_ITEM_WOOD_BOW);
                    arrowBag.SetActive(true);
                    isEquippedRemoteWeapon = true;
                } else {
                    if (rightHandResolve.GetLabel().Equals(EQUIPMENT_RIGHT_ITEM_WOOD_BOW)
                    || rightHandResolve.GetLabel().Equals(EQUIPMENT_RIGHT_ITEM_IRON_BOW)
                    || rightHandResolve.GetLabel().Equals(EQUIPMENT_RIGHT_ITEM_GOLD_BOW)) {
                        leftHandResolve.SetCategoryAndLabel(leftHandResolve.GetCategory(), EQUIPMENT_LEFT_OR_RIGHT_ITEM_NONE);
                        arrowBag.SetActive(false);
                        isEquippedRemoteWeapon = false;
                    }
                    rightHandResolve.SetCategoryAndLabel(rightHandResolve.GetCategory(), equipmentLevel);
                }
                break;
            default:
                break;
        }
    }


}



