using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour {


    #region Singleton

    public static Inventory instance;
    public EventSystem myEvent;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion

    // Callback which is triggered when
    // an item gets added/removed.
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 13;  // Amount of slots in inventory

    // Current list of items in inventory
    public List<Item> items = new List<Item>();
    public List<int> quantity = new List<int>();

    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(GameObject.Find("InventorySlot_1"));
    }

    public void Close()
    {
        myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    // Add a new item. If there is enough room we
    // return true. Else we return false.
    public bool Add(Item item)
    {
        if (items.Contains(item))
        {
            quantity[items.IndexOf(item)] += 1;
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
            return true;
        }
        else
        {

            // Check if out of space
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }

            items.Add(item);    // Add item to list
            quantity.Add(1);
            // Trigger callback
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();

            return true;
        }
    }

    // Remove an item
    public void Remove(Item item)
    {
        int index = items.IndexOf(item);
        if ( quantity[index] > 1)
        {
            quantity[index] -= 1;
        } else
        {
            items.Remove(item);
            quantity.RemoveAt(index);
        }

        // Trigger callback
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

}
