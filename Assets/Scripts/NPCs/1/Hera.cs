using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hera : NPC
{

    [SerializeField]
    private GameObject quests;

    [SerializeField]
    private string questName;

    [SerializeField]
    private string NamePerso;

    [SerializeField]
    private int nbQuestMax;
    private Quest myQuest { get; set; }

    // Use this for initialization
    void Start()
    {
        myDialog = GetComponent<NPC_dialogue>();
    }

    public override void Interact()
    {
        if (!questActivated)
        {
            StartCoroutine(AssignQuest());
            return;
        }
        else if (questActivated && !questFinished)
        {
            StartCoroutine(CheckQuest());
            return;
        }

    }

    public override IEnumerator AssignQuest()
    {
        switch (nbProgress)
        {
            case (0):
                myDialog.RunDialogue(0, NamePerso);
                yield return new WaitWhile(() => myDialog.onDialogue);
                myQuest = (Quest)quests.AddComponent(System.Type.GetType(questName));
                myQuest.AddGoal(0);                
                questActivated = true;
                break;

            case (1):
                //myDialog.RunDialogue(3, name);
                myQuest.AddGoal(1);
                break;
        }
        Debug.Log("QuestAssign");
        
    }

    public override IEnumerator CheckQuest()
    {
        if (myQuest.Completed)
        {
            if (nbProgress < nbQuestMax)
            {
                Debug.Log("tu as finis une étape !");
                nbProgress++;
                //Dialogue
               // yield return new WaitWhile(() => myDialog.onDialogue);
                StartCoroutine(AssignQuest());
            }
            else
            {
                //Dialogue;
                Debug.Log("Tu as tout finis !");
                //yield return new WaitWhile(() => myDialog.onDialogue);
                questFinished = true;
            }
        }
        else
        {
            //myDialog.RunDialogue(nbNodeDialogue);
            yield return new WaitWhile(() => myDialog.onDialogue);
            Debug.Log("T'as pas finit gros");
        }
    }
}
