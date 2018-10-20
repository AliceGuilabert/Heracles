using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleGoal : Goal
{
    public string ItemName { get; set; }
    private string baseDescription;

    public CollectibleGoal(Quest quest, string itemName, string description, bool completed, int currentAmount, int requieredAmount)
    {
        this.Quest = quest;
        this.ItemName = itemName;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requieredAmount;
        this.baseDescription = description;
        updateDescription(baseDescription);
    }

    public override void Init()
    {
        base.Init();
        OnPickUp.OnItemPicked += ItemPickedUp;
        LevelManager.OnItemSeen += ItemPickedUp;
    }

    void ItemPickedUp(Item item)
    {
        if (item.name == this.ItemName)
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