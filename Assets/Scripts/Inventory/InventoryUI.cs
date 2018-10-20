using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    List<Transform> slots = new List<Transform>();
    List<Item> items = new List<Item>();
    private Inventory myInvent;

    // Use this for initialization
    void Start () {
        myInvent = Inventory.instance;
        myInvent.onItemChangedCallback += UpdateUI;
        for (int i = 0; i < transform.childCount; i++)
        {
            slots.Add(transform.GetChild(i));
            items.Add(null);

        }
        UpdateUI();
    }

    public void AddItem(int index, Item item)
    {
        slots[index].GetComponent<Image>().sprite = item.icon;
        items[index] = item;
    }

    public void ClearSlot(int index)
    {
        slots[index].GetComponent<Image>().sprite = null;
        slots[index].GetChild(0).GetComponent<Text>().text = "";
        items[index] = null;
    }

    public void UpdateUI()
    {
        // Loop through all the slots
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < myInvent.items.Count)  // If there is an item to add
            {
                AddItem(i, myInvent.items[i]);   // Add it
                if (myInvent.quantity[i] > 1)
                {
                    slots[i].GetChild(0).GetComponent<Text>().text = myInvent.quantity[i].ToString();
                } else
                {
                    slots[i].GetChild(0).GetComponent<Text>().text = "";
                }
            }
            else
            {
                // Otherwise clear the slot
                ClearSlot(i);
            }
        }
    }
    public void UseItem(int index)
    {
        if ( items[index] != null)
        {
                items[index].Use();         
        }
    }

   
}
