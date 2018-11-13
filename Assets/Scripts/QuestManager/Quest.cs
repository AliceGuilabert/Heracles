using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Quest : MonoBehaviour {
    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string QuestName { get; set; }
    public Item ItemReward { get; set; }
    public bool Completed { get; set; }


    public void CheckGoals()
    {
        Completed = true;
        foreach (Goal g in Goals)
        {
            if(g.isActive && !g.Completed)
            {
                Completed = false;
            }
        }
    }

    void GiveReward()
    {
        if(ItemReward != null)
        {
            Inventory.instance.Add(ItemReward);
        }
    }

    public virtual void AddGoal(int nb) {}
}
