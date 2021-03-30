using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MoveBag : MonoBehaviour, IDragHandler {
    public Canvas canvas;
    RectTransform currentRect;

    public void OnDrag(PointerEventData eventData) {
        currentRect.anchoredPosition += eventData.delta;
    }

    void Awake() {
        currentRect = GetComponent<RectTransform>();
    }

    public void clickCloseBtn(){
        PlayerController.Instance.IsOpenbag = false;
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

}
