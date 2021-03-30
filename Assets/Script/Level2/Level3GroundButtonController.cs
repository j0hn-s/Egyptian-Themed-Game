using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3GroundButtonController : MonoBehaviour
{   

    
    [SerializeField] private GameObject groundDoor;
    private Animator groundDoorAnimator;

    [SerializeField] private Camera playerCamera;
    
    [SerializeField] private GameObject tempCollider;

    private static bool isGroundLeftBtnClicked = false;
    private static bool isGroundRightBtnClicked = false;

    private bool flag = true;

    // Start is called before the first frame update
    void Start()
    {
        groundDoorAnimator = groundDoor.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGroundRightBtnClicked && isGroundLeftBtnClicked && flag) {
            StartCoroutine(Shake(0.5f, 0.1f));
            tempCollider.SetActive(false);
            groundDoor.SetActive(false);
            // groundDoorAnimator.SetBool("isOpen", true);
            flag = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (this.name == "GroundLeftButton" && other.name == "LeftSarcophagus"){
            isGroundLeftBtnClicked = true;
        } else if (this.name == "GroundRightButton" && other.name == "RightSarcophagus") {
            isGroundRightBtnClicked = true;
        }
    }

    public IEnumerator Shake(float duration, float magnitude) {
        Vector3 orignalPosition = playerCamera.gameObject.transform.position;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            if (duration % 2 == 0) {
                playerCamera.gameObject.transform.position += new Vector3(x, y, 0f);
            } else {
                playerCamera.gameObject.transform.position -= new Vector3(x, y, 0f);
            }
            elapsed += Time.deltaTime;
            yield return 0;
        }
        playerCamera.gameObject.transform.position = orignalPosition;
    }
}
