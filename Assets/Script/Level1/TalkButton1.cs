using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton1 : MonoBehaviour {
    public GameObject Button;
    public GameObject talkUI;

    public bool chooseRight = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Button.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            Button.SetActive(false);
        }
    }

    private void Update() {
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.R)) {
            talkUI.SetActive(true);
            chooseRight = true;
        }
        if (chooseRight) {
            Invoke("destoryTalk", 8f);
        }
    }

    void destoryTalk() {
        Destroy(gameObject);
    }

}