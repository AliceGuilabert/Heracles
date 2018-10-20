using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour {

    public int damageToGive;
    public GameObject touchedParticle;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "HitBox")
        {
            Instantiate(touchedParticle, other.transform.position, other.transform.rotation);
            other.GetComponentInParent<EnemyHealthManager>().giveDamage(damageToGive);
        }
    }
}
