using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    
    public string nameItem;
    public Sprite icon;
    public bool isConsommable;
    public bool isEquipable;


    public void Use()
    {
        Debug.Log("Object utilisé !");
        if(isConsommable)
        {
            Debug.Log("Glouglou");
            Inventory.instance.Remove(this);
        }
        else if(isEquipable)
        {
            Debug.Log("Equipé !");
            Inventory.instance.Remove(this);
        } else
        {
            Debug.Log("Je ne peux pas utiliser cet objet");
        }
    }

    /*
    public void UseDestroy()
    {
        Debug.Log("Object utilisé !");
        if (isConsommable)
        {
            Debug.Log("Glouglou");
            Inventory.instance.Remove(this);
        }
        else if (isEquipable)
        {
            Debug.Log("Equipé !");
            Inventory.instance.Remove(this);
        }
        else
        {
            Debug.Log("Je ne peux pas utiliser cet objet");
        }
    }*/
}
