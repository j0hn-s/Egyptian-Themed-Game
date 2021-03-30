using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailureController : MonoBehaviour {
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            PlayerController.Instance.DestoryGameObject();
            SceneManager.LoadScene(0);
        }
    }
}
