using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {

    public string nameEnemy;
    public int maxEnemyHealth;
    private float enemyHealth;
    public bool alive { get; set; }
    private Animator myAnim;

    public Image healthBar;

    public delegate void EnemyEventHandler(EnemyHealthManager enemy);
    public static event EnemyEventHandler OnEnemyDeath;

    // Use this for initialization
    void Start () {
        myAnim = GetComponent<Animator>();
        enemyHealth = maxEnemyHealth;
        alive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(enemyHealth <= 0 && alive)
        {
            myAnim.SetTrigger("isDead");
            alive = false; 
            GetComponent<BoxCollider2D>().enabled = !GetComponent<BoxCollider2D>().enabled;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().gravityScale = 0;

            if(OnEnemyDeath != null)
            OnEnemyDeath(this);
        }
	}

    public void giveDamage(int damageReceive)
    {
        enemyHealth -= damageReceive;
        healthBar.fillAmount = enemyHealth / maxEnemyHealth;
    }
}
