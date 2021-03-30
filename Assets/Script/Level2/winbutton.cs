using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winbutton : MonoBehaviour
{

    public void backToMainMenu()
    {
        PlayerController.Instance.DestoryGameObject();
        SceneManager.LoadScene(0);
    }

}