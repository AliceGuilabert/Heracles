using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : Goal {
    public string EnemyName { get; set; }
    private string baseDescription;
    
    public KillGoal(Quest quest, string enemyName, string description, bool completed, int currentAmount, int requieredAmount)
    {
        this.Quest = quest;
        this.EnemyName = enemyName;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requieredAmount;
        this.baseDescription = description;
        updateDescription(baseDescription);
    }

    public override void Init()
    {
        base.Init();
        EnemyHealthManager.OnEnemyDeath += EnemyDied;
    }

    void EnemyDied(EnemyHealthManager enemy)
    {
        
        if(enemy.nameEnemy == this.EnemyName)
        {
            this.CurrentAmount++;
            updateDescription(baseDescription);
            GameObject.FindObjectOfType<QuestUI>().UpdateUI();
            Evaluate();
        }
    }

    void updateDescription(string description)
    {
        this.Description = description + " " + CurrentAmount + "/" + RequiredAmount;
    }
}
