/*
 * @Date: 2020-11-20 19:13:47
 * @Author: Zilin Zhang
 * @LastEditors: Zilin Zhang
 * @LastEditTime: 2020-12-01 06:55:04
 * @FilePath: /SE_CW2_Group8_Yellow/Assets/Script/Base/MonoSingleton.cs
 * @Description: The base class of Mono Singleton Object
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : Component {
    private static T _instance;

    public static T Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<T>();
                if (_instance == null) {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    public virtual void Awake() {
        if (_instance == null) {
            _instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void DestoryGameObject(){
        Destroy(this.gameObject);
    } 

    public void DestoryScriptInstance(){
        Destroy(this);
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
    }
}
