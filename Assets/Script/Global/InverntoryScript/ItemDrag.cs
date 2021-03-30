using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Transform originParent;
    public inventory mybag;
    private int currentItemID;// Current item ID
    public void OnBeginDrag(PointerEventData eventData)
    {   
        originParent = transform.parent;
        currentItemID = originParent.GetComponent<Slot>().slotID;
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        // Turn off blocking rays
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {  
        if(eventData.pointerCurrentRaycast.gameObject != null)
        {
             //  Determine whether the name of the object passed by the mouse is ItemImage, if it is to swap positions
            if(eventData.pointerCurrentRaycast.gameObject.name == "Item Image")
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
                //The item storage location of itemList changes
                var temp = mybag.itemList[currentItemID];
                mybag.itemList[currentItemID] = mybag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
                mybag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;

                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originParent);
                // Turn on blocking rays so that moving items can be selected again
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
            if(eventData.pointerCurrentRaycast.gameObject.name == "item(Clone)")
            {
                //else hang directly under the detected slot
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                //The item storage location of itemList changes
                mybag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = mybag.itemList[currentItemID];
                // Solve problems in origin place
                if(eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID != currentItemID)
                {
                    mybag.itemList[currentItemID] = null;
                }
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
        }

        // Move the mouse to a place other than the grid, and automatically return
        transform.SetParent(originParent);
        transform.position = originParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        
    }

}
