using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionText : MonoBehaviour {

    Text myText;
    Animator myAnimator;

    private float hight;

	// Use this for initialization
	void Start () {
        myText = GetComponent<Text>();
        myAnimator = GetComponent<Animator>();
        Interactable.OnTheInteractablePlace += DisplayInteractableText;
        Interactable.ExitTheInteractablePlace += CleanText;
        OnPickUp.OnThePickUpPlace += DisplayPickUpText;
        OnPickUp.ExitThePickUpPlace += CleanText;
        hight = 3;
    }

    void DisplayInteractableText(GameObject myObject)
    {
        myText.text = "Interagir";
        myText.transform.position = new Vector2(myObject.transform.position.x,
            myObject.transform.position.y + hight);
    }

    void DisplayPickUpText(GameObject myObject)
    {
        myText.text = "Fouiller";
        myText.transform.position = new Vector2(myObject.transform.position.x,
            myObject.transform.position.y + hight);
    }

    void CleanText(GameObject myObject)
    {
        myText.text ="";
    }
	

}
