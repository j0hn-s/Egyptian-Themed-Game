using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtonController : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject WarningMessage;

    public void PauseGame() {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }
    public void ResumeGame() {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void showWarningMessage() {
        WarningMessage.SetActive(true);
    }

    public void hideWarningMessage() {
        WarningMessage.SetActive(false);
    }

    public void backToMainMenu() {
        PlayerController.Instance.DestoryGameObject();
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
