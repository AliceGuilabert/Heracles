using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

    public int damageToGive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Equals("Player") && GetComponentInParent<EnemyHealthManager>().alive)
        {
            collision.GetComponent<PlayerHealthManager>().RemoveLife(damageToGive);
        }
    }

}
