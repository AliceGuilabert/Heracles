using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    LevelManager1 mylevel;
    public string NameItem;
    private bool firstInteraction { get; set; }

	// Use this for initialization
	void Start () {
        mylevel = FindObjectOfType<LevelManager1>();
        firstInteraction = true;
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Equals("Player") && Input.GetKeyDown(KeyCode.RightShift))
        {
            mylevel.HandleInteraction(firstInteraction, NameItem);
            firstInteraction = false;
        }
    }
}
