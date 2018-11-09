using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NPC_dialogue : MonoBehaviour {

    public PlayerController myPlayer;
    public bool onDialogue;

    private Dialogue dia;

    private GameObject dialogue_window;

    private GameObject node_text;
    private GameObject option_1;
    private GameObject option_2;
    private GameObject NameBox;
    // private GameObject skip;

    private int selected_option = -2;
    public string DialogueDataFilePath;

    private EventSystem myEvent;

	// Use this for initialization
	void Start () {

        myPlayer = FindObjectOfType<PlayerController>();
        myEvent = FindObjectOfType<EventSystem>();
        onDialogue = false;

        dia = Dialogue.LoadDialogue("Assets/" + DialogueDataFilePath);
        dialogue_window = GameObject.Find("Dialogue");

        node_text = GameObject.Find("Text_DiaNodeText");
        option_1 = GameObject.Find("Button_Option1");
        option_2 = GameObject.Find("Button_Option2");
        NameBox = GameObject.Find("NameImage");

        dialogue_window.SetActive(false);

	}
	
	public void RunDialogue(int nodeStart, string NamePerso)
    {
        NameBox.GetComponentInChildren<Text>().text = NamePerso;
        StartCoroutine(run(nodeStart));
    }

    public void SetSelectedOption(int x)
    {
        selected_option = x;
    }

    public IEnumerator run(int nodeStart)
    {
        myPlayer.pause = true;
        onDialogue = true;
        dialogue_window.SetActive(true);

        myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().firstSelectedGameObject = option_1;

        //create a indexer, set it to 0 - the dialogue Start node.
        int node_id = nodeStart;

        //while the next node is not an exit node, traverse the dialogue tree based on
        // the user input
        while(node_id != -1)
        {
            myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            display_node(dia.Nodes[node_id]);

            selected_option = -2;
            while (selected_option == -2)
            {
                yield return new WaitForSeconds(0.25f);
            }

            node_id = selected_option;
        }

        //RAJOUTER UN CODE DE SORTIE ET RAPPELER NPC POUR QU'IL prenne en charge ce qui va se passer.

        dialogue_window.SetActive(false);
        myPlayer.pause = false;
        onDialogue = false;
    }

    private void display_node(DialogueNode node)
    {
        option_1.SetActive(false);
        option_2.SetActive(false);
        StartCoroutine(AnimateText(node));
        //node_text.GetComponent<Text>().text = node.Text;

    }

    private void display_options(DialogueNode node)
    {
        for (int i = 0; i < node.Options.Count; i++)
        {
            switch (i)
            {
                case 0:
                    set_option_button(option_1, node.Options[i]);
                    break;
                case 1:
                    set_option_button(option_2, node.Options[i]);
                    break;
            }
        }

        myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(option_1);
    }

    private void set_option_button(GameObject button, DialogueOption opt)
    {
        button.SetActive(true);
        button.GetComponentInChildren<Text>().text = opt.Text;
        button.GetComponent<Button>().onClick.AddListener(delegate { SetSelectedOption(opt.DestinationNodeID); });
    }

    private IEnumerator AnimateText(DialogueNode node)
    {
        string strComplete = node.Text;
        int i = 0;
        node_text.GetComponent<Text>().text = "";
        while (i < strComplete.Length)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StopCoroutine("AnimateText");
                StartCoroutine(printText(node));
                yield break;
            }
            node_text.GetComponent<Text>().text += strComplete[i++];
            yield return new WaitForSeconds(0.01F);
        }

        yield return new WaitForSeconds(0.3F);
        display_options(node);

    }

    private IEnumerator printText(DialogueNode node)
    {
        node_text.GetComponent<Text>().text = node.Text;
        yield return new WaitForSeconds(0.3F);

        display_options(node);
    }
}
