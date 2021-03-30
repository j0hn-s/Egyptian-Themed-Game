using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController instance;

    public float speed;

    private Transform target;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        target = PlayerController.Instance.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
        if (target != null) {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), speed * Time.deltaTime);
        }
    }

    public void ChangeTarget(Transform newTarget) {
        target = newTarget;
    }

}
