using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour {

    public int healthMax;
    public float currentHealth { get; set; }
   // private bool isAlive;

    SpriteRenderer mySprite;
    public GameObject hitPlayerParticle;

    public int frameInvicibility;
    private int frameSpendLastHit;
    private bool isInvicible;

    public Image healthBar;

    // Use this for initialization
    void Start () {
        currentHealth = healthMax;
        mySprite = GetComponent<SpriteRenderer>();
        isInvicible = false;
       // isAlive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentHealth <= 0)
        {
           // isAlive = false;
            //animation de mort
            //respawn
        }

        if (isInvicible)
        {
            if(frameSpendLastHit > frameInvicibility )
            {
                isInvicible = false;
            }
            else if (frameSpendLastHit > 0.3f * frameInvicibility)
            {
                mySprite.color = new Color(mySprite.color.r, mySprite.color.g, mySprite.color.b, 1f);
            }
            else if (frameSpendLastHit > 0.2f * frameInvicibility)
            {
                mySprite.color = new Color(mySprite.color.r, mySprite.color.g, mySprite.color.b, 0f);
            }
            else if (frameSpendLastHit > 0.1f * frameInvicibility)
            {
                mySprite.color = new Color(mySprite.color.r, mySprite.color.g, mySprite.color.b, 1f);
            }
            else if (frameSpendLastHit == 0)
            {
                mySprite.color = new Color(mySprite.color.r, mySprite.color.g, mySprite.color.b, 0f);
            }
            frameSpendLastHit++;
        }
	}

    public void AddLife(int lifeToGive)
    {
        currentHealth += lifeToGive;
        if (currentHealth > healthMax)
        {
            currentHealth = healthMax;
        }
        healthBar.fillAmount = currentHealth / healthMax;
    }

    public void RemoveLife(float lifeToRemove)
    {
        if(!isInvicible)
        {
            currentHealth -= lifeToRemove;
            Instantiate(hitPlayerParticle, transform.position, transform.rotation);
            frameSpendLastHit = 0;
            isInvicible = true;
            healthBar.fillAmount = currentHealth / healthMax;

            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
        } else
        {
            return;
        }
    }
}
