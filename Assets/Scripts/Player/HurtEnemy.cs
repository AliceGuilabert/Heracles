using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour {

    public int damageToGive;
    public GameObject touchedParticle;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "HitBox" && other.GetComponentInParent<EnemyHealthManager>().alive)
        {
            Instantiate(touchedParticle, other.transform.position, other.transform.rotation);
            other.GetComponentInParent<EnemyHealthManager>().giveDamage(damageToGive);
        }
    }
}
