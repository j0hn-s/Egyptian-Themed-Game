using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrancePoint : MonoBehaviour
{

    [SerializeField] private string entrancePwd;

    [SerializeField] private GameObject failureDialog;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.Instance.scenePwd == entrancePwd) {
            PlayerController.Instance.transform.position = transform.position;
            PlayerController.Instance.FailureDialog = failureDialog;
        } else {
            Debug.Log("Wrong password");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
