using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    LevelManager1 mylevel;
    public string NameItem;
	// Use this for initialization
	void Start () {
        mylevel = FindObjectOfType<LevelManager1>();
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Equals("Player") && Input.GetKeyDown(KeyCode.RightShift))
        {
            mylevel.HandleInteraction(NameItem);
        }
    }
}
