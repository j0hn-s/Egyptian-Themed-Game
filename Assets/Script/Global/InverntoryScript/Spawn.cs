using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    #region Singleton

	public static Spawn instance;

	void Awake ()
	{
		if (instance != null)
		{
			Debug.LogWarning("More than one instance of Spwan found!");
			return;
		}

		instance = this;
	}

	#endregion
    public GameObject armour;
    public GameObject sheid;
    public GameObject Bow;
    public GameObject lefthand;
    public GameObject redpotion;
    private Transform Player;
    // Start is called before the first frame update
    void Start()
    {   
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    public void SpawnItem(EquipmentType equipmentType,string equipmentString)
    {
        
        Vector2 playerPos = new Vector2(Player.position.x,Player.position.y + 2);
        switch(equipmentType){
            case EquipmentType.armour:
            GameObject.Instantiate(armour,playerPos,Quaternion.identity);
            break;
            case EquipmentType.rightHand:
            if(equipmentString.Equals("woodBow"))
            {
                GameObject.Instantiate(Bow,playerPos,Quaternion.identity);}
            else{
          
            GameObject.Instantiate(sheid,playerPos,Quaternion.identity);
            }
            break;
            case EquipmentType.leftHand:
            GameObject.Instantiate(lefthand,playerPos,Quaternion.identity);
            break;
            case EquipmentType.redpotion:
            GameObject.Instantiate(redpotion,playerPos,Quaternion.identity);
            break;
        }
        
    }



}
