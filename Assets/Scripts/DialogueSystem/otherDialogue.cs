using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class otherDialogue : MonoBehaviour
{

    public PlayerController myPlayer;
    public bool onDialogue;

    private Dialogue dia;

    private GameObject dialogue_window;

    private GameObject node_text;
    private GameObject Exit;

    private int selected_option = -2;
    public string DialogueDataFilePath;

    private EventSystem myEvent;

    // Use this for initialization
    void Start()
    {
        myPlayer = FindObjectOfType<PlayerController>();
        myEvent = FindObjectOfType<EventSystem>();
        onDialogue = false;

        dia = Dialogue.LoadDialogue("Assets/" + DialogueDataFilePath);
        dialogue_window = GameObject.Find("Infos");

        node_text = GameObject.Find("Text_node");
        Exit = GameObject.Find("Button_exit");

        dialogue_window.SetActive(false);
    }

    public void RunDialogue(int nodeStart)
    {
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

        myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().firstSelectedGameObject = Exit;

        //create a indexer, set it to 0 - the dialogue Start node.
        int node_id = nodeStart;

        //while the next node is not an exit node, traverse the dialogue tree based on
        // the user input
        while (node_id != -1)
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

        dialogue_window.SetActive(false);
        myPlayer.pause = false;
        onDialogue = false;
    }

    private void display_node(DialogueNode node)
    {
        Exit.SetActive(false);
        StartCoroutine(AnimateText(node));
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
               StartCoroutine(printText(node));
                yield break;
            }
            node_text.GetComponent<Text>().text += strComplete[i++];
            yield return new WaitForSeconds(0.02F);
        }

        yield return new WaitForSeconds(0.3F);

        set_option_button(Exit, node.Options[0]);
        myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(Exit);
    }

    private IEnumerator printText(DialogueNode node)
    {
        node_text.GetComponent<Text>().text = node.Text ;
        yield return new WaitForSeconds(0.3F);

        set_option_button(Exit, node.Options[0]);
        myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(Exit);
    }

}
