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
                Debug.Log("Appel");
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
                Debug.Log("Appelle ici");
                StartCoroutine(Lion());
                break;
            default:
                Debug.LogError("Action non valide");
                break;
        }
    }


    IEnumerator Lion()
    {
        Debug.Log("coucou");
        CameraScript myCam = FindObjectOfType<CameraScript>();
        myCam.isParalax = false;
        myPlayer.pause = true;

        while (myCam.transform.position.x < 2.91f)
        {
            myCam.transform.position = new Vector3(myCam.transform.position.x + 0.09f,
                myCam.MinPosition.y, myCam.transform.position.z);
            yield return new WaitForFixedUpdate();
        }

        mydialog.RunDialogue(7);
        yield return new WaitWhile(() => mydialog.onDialogue);

        Debug.Assert(GameObject.Find("Lion").GetComponent<Rigidbody2D>() != null, "Pas trouvé le lion");
        GameObject.Find("Lion").GetComponent<Rigidbody2D>().velocity = new Vector2(-8, 13);
        GameObject.Find("Lion").GetComponentInChildren<Animator>().SetTrigger("Attack");

        yield return new WaitForSeconds(2f);

        myPlayer.GetComponent<PlayerHealthManager>().RemoveLife(myPlayer.GetComponent<PlayerHealthManager>().currentHealth - 1);
        myPlayer.manual = true;
        yield return new WaitForSeconds(1f);
        myPlayer.transform.localScale = new Vector2(-myPlayer.transform.localScale.x, myPlayer.transform.localScale.y);
        myPlayer.myRigid.velocity = new Vector2(-18, 25);
        myPlayer.myAnim.Play("Jump Up");     

        yield return new WaitForSeconds(2);


        Teleportation myTeleport = GameObject.Find("ActionLionGrotte").GetComponent<Teleportation>();
        StartCoroutine(myTeleport.TeleportAnimation(myPlayer.transform));
        yield return new WaitWhile(() => myTeleport.changing);
        myCam.isParalax = true;
        myPlayer.myAnim.Play("Idle");
        myPlayer.manual = false;

        yield return new WaitWhile(() => myTeleport.hasBegan);

        mydialog.RunDialogue(8);
        yield return new WaitWhile(() => mydialog.onDialogue);

    }

}
