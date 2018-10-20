using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTrigger : MonoBehaviour {

    public string namePlace;

    public delegate void PlaceEventHandler(PlaceTrigger place);
    public static event PlaceEventHandler OnThePlace;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Player")) {
            if (OnThePlace != null)
                OnThePlace(this);
        }
    }

}
