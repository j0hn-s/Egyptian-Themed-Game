using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveItem : MonoBehaviour
{
    public Transform thisItem;
    public inventory playerInventory;
    private int Itemid;

    public void Remove()
    {
        thisItem = transform.parent;
        Itemid = thisItem.GetComponent<Slot>().slotID;
        string equipmentString = playerInventory.itemList[Itemid].equipmentString;
        EquipmentType equipmentType = playerInventory.itemList[Itemid].equipSlot;
        playerInventory.itemList[Itemid] = null;
        Spawn.instance.SpawnItem(equipmentType,equipmentString);
        InventoryManage.RefreshItem();
        
    }




}
