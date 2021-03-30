using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManage : MonoBehaviour {
    public static InventoryManage instance;

    public inventory mybag;
    public GameObject slotGrid;
    public GameObject emptyprefab;
    public Text itemInfo;

    // Manage the generated 18 slots
    public List<GameObject> slots = new List<GameObject>();
    private void Awake() {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void OnEnable() {
        RefreshItem();
        instance.itemInfo.text = "";
    }
    public static void UpdateItemInfo(string itemDescription) {
        instance.itemInfo.text = itemDescription;
    }


    public static void RefreshItem() {
        // Delete the subset objects under slotgrid cyclically
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++) {
            if (instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            instance.slots.Clear();
        }

        // Regenerate the slot corresponding to the item in mybag
        for (int i = 0; i < instance.mybag.itemList.Count; i++) {
            // CreatNewItem(instance.mybag.itemList[i]);
            instance.slots.Add(Instantiate(instance.emptyprefab));
            instance.slots[i].transform.SetParent(instance.slotGrid.transform);
            instance.slots[i].GetComponent<Slot>().slotID = i;
            instance.slots[i].GetComponent<Slot>().SetuoSlot(instance.mybag.itemList[i]);
        }
    }

    public static void useItem(int itemEquip, string equipmentString) {
        if (itemEquip == 0) { // left hand 
            PlayerController.Instance.ChangeEquipments(EquipmentType.leftHand, equipmentString);
        } else if (itemEquip == 1) { // body / armour
            PlayerController.Instance.ChangeEquipments(EquipmentType.armour, equipmentString);
        } else if (itemEquip == 2) { // right hand
            PlayerController.Instance.ChangeEquipments(EquipmentType.rightHand, equipmentString);
        } else if (itemEquip == 3) { // other
            Debug.Log("Restore Health");
            
            PlayerController.Instance.RestoreHealth();
        }
    }
}