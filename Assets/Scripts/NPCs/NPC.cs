using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour {

    public NPC_dialogue myDialog { get; set; }
    protected bool questActivated;
    protected bool questFinished;
    protected int nbProgress;

    private void Start()
    {
        questActivated = false;
        questFinished = false;
        nbProgress = 0;
    }

    public abstract void Interact();
    public abstract IEnumerator AssignQuest();
    public abstract IEnumerator CheckQuest();

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Equals("Player") && Input.GetKeyDown(KeyCode.RightShift) && !myDialog.onDialogue)
        {
            Interact();
        }
    }
}
