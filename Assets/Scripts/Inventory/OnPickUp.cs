using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPickUp : MonoBehaviour
{
    public Item[] objects;
    public Inventory myInvent;
    EnemyHealthManager myHealth;
    bool picked;

    public delegate void ItemPickEventHandler(Item item);
    public static event ItemPickEventHandler OnItemPicked;

    private void Start()
    {
        picked = false;
        myHealth = GetComponentInParent<EnemyHealthManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Equals("Player") && !myHealth.alive && Input.GetKeyDown(KeyCode.RightShift))
        {
            PickOnEnemy();
        }
    }

    public void PickOnEnemy()
    {
        if (!picked)
        {
            Debug.Log("Rentre");
            //Faire l'affichage
            foreach (Item item in objects)
            {
                Debug.Log("Ajouté");
                if (OnItemPicked != null)
                    OnItemPicked(item);
                myInvent.Add(item);

            }
            //picked = true;
        } else
        {
            //MESSAGE
        }
    }

}