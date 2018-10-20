using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal {
    public string Description { get; set; }
    public bool Completed { get; set; }
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }

    public Quest Quest { get; set; }

    public bool isActive;
    public QuestUI myQuestUI { get; set; }


    public virtual void Init()
    {
        isActive = false;
        GameObject.FindObjectOfType<QuestUI>().Goals.Add(this);
        GameObject.FindObjectOfType<QuestUI>().UpdateUI();
    }

    public void Evaluate()
    {
        if(CurrentAmount >= RequiredAmount)
        {
            Complete();
        }

    }

    public void Complete()
    {
        Completed = true;
        GameObject.FindObjectOfType<QuestUI>().Goals.Remove(this);
        GameObject.FindObjectOfType<QuestUI>().UpdateUI();
        Quest.CheckGoals();
    }
}
