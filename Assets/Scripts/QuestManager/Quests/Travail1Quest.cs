using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Travail1Quest : Quest
{

    public Item item;

    // Use this for initialization
    void Start()
    {
        QuestName = "Travail1";
        ItemReward = item;
    }

    public override void AddGoal(int nb)
    {
        switch (nb)
        {
                case (0):
                    Goals.Add(new CollectibleGoal(this, "Preuve", "Récolter des indices", false, 0, 3));
                    Goals.Add(new PointGoal(this, "Grotte", "Aller à la caverne", false, 0, 1));
                    Goals.ForEach(g => g.Init());
                    break;

                case (1):
                    Debug.Log("Newquete");
                    Goals.ForEach(g => g.isActive = false);
                    Completed = false;
                    Goals.Add(new PointGoal(this, "Grotte", "Entrer dans la grotte", false, 0, 1));
                    Goals[2].Init();
                    break;

                case (2):
                    Debug.Log("Newquete2");
                    Goals.ForEach(g => g.isActive = false);
                    Completed = false;
                    Goals.Add(new PointGoal(this, "ActionLionGrotte", "Explorer la grotte", false, 0, 1));
                    Goals[3].Init();
                    break;
        }
    }

}
