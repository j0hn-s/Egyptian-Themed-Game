using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestoryEffectObject", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void DestoryEffectObject(){
        Destroy(gameObject);
    }

}
