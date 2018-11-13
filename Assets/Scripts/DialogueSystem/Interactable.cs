using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    LevelManager1 mylevel;
    public string NameItem;
    private bool firstTrigger;
    private bool firstInteraction { get; set; }

    public delegate void InteractableEventHandler(GameObject interactable);
    public static event InteractableEventHandler OnTheInteractablePlace;
    public static event InteractableEventHandler ExitTheInteractablePlace;

    // Use this for initialization
    void Start () {
        mylevel = FindObjectOfType<LevelManager1>();
        firstInteraction = true;
        firstTrigger = true;
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Equals("Player") && firstTrigger){
            if (OnTheInteractablePlace != null) OnTheInteractablePlace(this.gameObject); firstTrigger = false;
        }
        if (collision.name.Equals("Player") && Input.GetKeyDown(KeyCode.RightShift))
        {
            mylevel.HandleInteraction(firstInteraction, NameItem);
            firstInteraction = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals("Player"))
        {
            if (ExitTheInteractablePlace != null) ExitTheInteractablePlace(this.gameObject); firstTrigger = true;
        }
    }
}
