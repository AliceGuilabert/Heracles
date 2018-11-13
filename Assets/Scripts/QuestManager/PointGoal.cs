using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGoal : Goal
{
    public string PointName { get; set; }
    public delegate void OnPlaceArrivedEventHandler(string namePlace, int codeSortie);
    public static event OnPlaceArrivedEventHandler OnPlaceCheck;

    public PointGoal(Quest quest, string pointName, string description, bool completed, int currentAmount, int requieredAmount)
    {
        this.Quest = quest;
        this.PointName = pointName;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requieredAmount;
    }

    public override void Init()
    {
        base.Init();
        PlaceTrigger.OnThePlace += HotspotOn;
    }

    void HotspotOn(PlaceTrigger place)
    {
        if (!isActive) return;

        if (place.namePlace == this.PointName)
        {
            this.CurrentAmount++;

            Completed = true;
            Quest.CheckGoals();

            if (Quest.Completed)
            {
                GameObject.FindObjectOfType<QuestUI>().Goals.Remove(this);
                GameObject.FindObjectOfType<QuestUI>().UpdateUI();
                //c'est bon go dialogue;
                if (OnPlaceCheck != null)  OnPlaceCheck(place.namePlace, 1);
                
            }
            else
            {
                this.CurrentAmount--;
                //C'est pas bon
                if (OnPlaceCheck != null) OnPlaceCheck(place.namePlace, 0);
                Completed = false;
            }
        }
    }

}