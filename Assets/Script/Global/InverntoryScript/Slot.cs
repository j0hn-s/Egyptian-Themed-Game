using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slot : MonoBehaviour {
    public int slotID; //Space ID is equal to item ID
    public item slotItem;
    public Image slotImage;

    public Text slotnume;
    public string slotInfo;
    public int slotType;

    public GameObject itemInslot;

    private string equipmentString;

    public void ItemOnClicked() {
        InventoryManage.UpdateItemInfo(slotInfo);
    }

    public void SetuoSlot(item item) {
        if (item == null) {
            itemInslot.SetActive(false);
            return;
        }
        slotImage.sprite = item.itemImage;
        slotnume.text = item.itemHeld.ToString();
        slotInfo = item.itemInfo;
        slotType = (int)item.equipSlot;
        equipmentString = item.equipmentString;
    }

    public void useItem() {
        switch (slotType) {
            case 0:
                InventoryManage.useItem(0, equipmentString);
                break;
            case 1:
                InventoryManage.useItem(1, equipmentString);
                break;
            case 2:
                InventoryManage.useItem(2, equipmentString);
                break;
            case 3:
                InventoryManage.useItem(3, equipmentString);
                break;
        }
    }


}
