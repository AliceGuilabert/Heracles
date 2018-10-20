using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuestTest : Quest {

    public Item item;

	// Use this for initialization
	void Start () {
        QuestName = "KillQuest";
        ItemReward = item;
	}

    public override void AddGoal(int nb)
    {
        switch(nb)
        {
            case (0):
                Goals.Add(new KillGoal(this, "Slime", "Kill 1 slimes", false, 0, 1));
                Goals.Add(new KillGoal(this, "slime", "Kill 2 vampire", false, 0, 2));
                Goals.ForEach(g => g.Init());
                break;

            case (1):
                Goals.Add(new KillGoal(this, "slime", "Kill 3 whatever", false, 0, 3));
                Goals[2].Init();
                break;
        }
    }
	
}
