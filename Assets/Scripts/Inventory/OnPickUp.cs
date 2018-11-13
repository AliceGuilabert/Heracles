using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPickUp : MonoBehaviour
{
    public Item[] objects;
    public Inventory myInvent;
    EnemyHealthManager myHealth;
    bool picked;
    bool firstTrigger;

    public delegate void ItemPickEventHandler(Item item);
    public static event ItemPickEventHandler OnItemPicked;

    public delegate void PickUpEventHandler(GameObject interactable);
    public static event PickUpEventHandler OnThePickUpPlace;
    public static event PickUpEventHandler ExitThePickUpPlace;

    private void Start()
    {
        picked = false;
        myHealth = GetComponentInParent<EnemyHealthManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Equals("Player") && firstTrigger && !myHealth.alive && !picked)
        {
            if (OnThePickUpPlace != null)
            {
                OnThePickUpPlace(this.gameObject);
                firstTrigger = false;
            }
        }
        if (collision.name.Equals("Player") && !myHealth.alive && Input.GetKeyDown(KeyCode.RightShift))
        {
            PickOnEnemy();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals("Player"))
        {
            if (ExitThePickUpPlace != null)
            {
                ExitThePickUpPlace(this.gameObject);
                firstTrigger = true;
            }
        }
    }

    public void PickOnEnemy()
    {
        if (!picked)
        {

            //Faire l'affichage
            foreach (Item item in objects)
            {
                Debug.Log("Ajouté");
                if (OnItemPicked != null)
                    OnItemPicked(item);
                myInvent.Add(item);

            }
            //picked = true;    --> A REACTIVER
        } else
        {
            //MESSAGE
        }
    }

}