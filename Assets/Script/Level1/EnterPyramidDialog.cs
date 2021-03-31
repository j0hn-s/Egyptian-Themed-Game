using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterPyramidDialog : MonoBehaviour {

    [SerializeField] private string newScenePwd;
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            PlayerController.Instance.scenePwd = newScenePwd;
            this.gameObject.SetActive(false); 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
