using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/New Item")]
public class item : ScriptableObject
{
   public string itemName;
   public Sprite itemImage;
   public int itemHeld;
   [TextArea]
   public string itemInfo;

   public EquipmentType equipSlot;

   public string equipmentString;

}
    /**
     * @Description: The Enum type to make change EquipmentType easier and avoid error
     * @version: 1.0.0
     * @Author: Zilin Zhang
     * @Date: 2020-11-27 03:34:48
     * @LastEditors: Zilin Zhang
     * @LastEditTime: 2020-11-27 03:34:48
     */
public enum EquipmentType {leftHand,armour,rightHand,redpotion}