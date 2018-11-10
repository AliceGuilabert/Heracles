using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager1 : MonoBehaviour {

    otherDialogue mydialog;
    LevelManager myLevel;
    Hera myNPC;
    PlayerController myPlayer;
    //Inventory myInventory;

    public List<Item> items = new List<Item>();

	// Use this for initialization
	void Start () {
        PointGoal.OnPlaceCheck += handlePlaceGoal;
        mydialog = GetComponent<otherDialogue>();
        myNPC = FindObjectOfType<Hera>();
        myPlayer = FindObjectOfType<PlayerController>();
       // myInventory = Inventory.instance;
        myLevel = GetComponent<LevelManager>();
    }

    public void handlePlaceGoal(string namePlace, int code)
    {
        switch (namePlace)
        {
            case ("Grotte"):
                mydialog.RunDialogue(code);
                if (code == 1)
                {
                    Debug.Log("go new truc");
                    myNPC.Interact();
                }
                break;
            case ("ActionLionGrotte"):
                ActionHandler(1);
                break;
            default:
                Debug.LogError("Place non valide");
                break;
        }
    }

    public void HandleInteraction(bool firstTime, string nameItem)
    {
        switch(nameItem)
        {
            case ("Preuve1"):
                mydialog.RunDialogue(2);
                if (firstTime) myLevel.InteractableGoal(items[0]);
                break;
            case ("Preuve2"):
                mydialog.RunDialogue(3);
                if (firstTime) myLevel.InteractableGoal(items[0]);
                break;
            case ("Preuve3"):
                mydialog.RunDialogue(4);
                if (firstTime) myLevel.InteractableGoal(items[0]);
                break;
            case ("Panneau"):
                mydialog.RunDialogue(5);
                break;
            case ("PanneauGrotte"):
                mydialog.RunDialogue(6);
                break;
            default:
                Debug.LogError("Interaction non valide");
                break;
        }
    }

    void ActionHandler(int code)
    {
        switch (code)
        {
            case (1):
                myPlayer.pause = true;
                GameObject.Find("Lion").GetComponent<Rigidbody2D>().AddForce(new Vector2(5, 5));
                break;
            default:
                Debug.LogError("ction non valide");
                break;
        }
    }

}
