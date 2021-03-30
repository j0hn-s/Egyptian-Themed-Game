using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destoryer : MonoBehaviour {
    // public GameObject talkUI;

    public TalkButton1 talkShow;

    private void Start() {
        talkShow = GameObject.FindGameObjectWithTag("NPC").GetComponent<TalkButton1>();
    }
    private void Update() {

        if (talkShow.chooseRight == true) {
            Destroy(gameObject);
        }
    }

}
