using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour {

    public List<Goal> Goals = new List<Goal>();
    private Text myText;

    // Use this for initialization
    void Start () {
        myText = GetComponent<Text>();
	}
	
	public void UpdateUI () {
        myText.text = "";
		for(int i =0; i<Goals.Count; i++)
        {
            if(i == Goals.Count - 1)
            {
                myText.text += Goals[i].Description;
            } else
            {
                myText.text += Goals[i].Description + "\n";
            }
            
        }
	}
}
