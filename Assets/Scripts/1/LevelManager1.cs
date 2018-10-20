using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager1 : MonoBehaviour {

    otherDialogue mydialog;
    LevelManager myLevel;
    Hera myNPC;
    //Inventory myInventory;

    public List<Item> items = new List<Item>();

	// Use this for initialization
	void Start () {
        PointGoal.OnPlaceCheck += handleDialogueGoal;
        mydialog = GetComponent<otherDialogue>();
        myNPC = FindObjectOfType<Hera>();
       // myInventory = Inventory.instance;
        myLevel = GetComponent<LevelManager>();
    }

    public void handleDialogueGoal(string namePlace, int code)
    {
        if(namePlace.Equals("Grotte"))
        {
            mydialog.RunDialogue(code);
            if (code == 1)
            {
                Debug.Log("go new truc");
                myNPC.Interact();
            }
            
        }   
    }

    public void HandleInteraction(string nameItem)
    {
        switch(nameItem)
        {
            case ("Preuve1"):
                mydialog.RunDialogue(2);
                myLevel.InteractableGoal(items[0]);
                break;
            case ("Preuve2"):
                mydialog.RunDialogue(3);
                myLevel.InteractableGoal(items[0]);
                break;
            case ("Preuve3"):
                mydialog.RunDialogue(4);
                myLevel.InteractableGoal(items[0]);
                break;
            case ("Panneau"):
                mydialog.RunDialogue(5);
                break;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
