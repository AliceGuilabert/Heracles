using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject checkPoint;

    private PlayerController player;
    public GameObject respawnParticle;

    public delegate void ItemSeenEventHandler(Item item);
    public static event ItemSeenEventHandler OnItemSeen;

    private float respawnDelay;
   // private Rigidbody2D playerRigid;
   // private float gravity;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
        respawnDelay = 0.7f;
        //playerRigid = player.GetComponent<Rigidbody2D>();
        //gravity = playerRigid.gravityScale;
	}
	
    public void InteractableGoal(Item item)
    {
        if (OnItemSeen != null)
            OnItemSeen(item);
    }

    public void RespawnPlayer()
    {

        StartCoroutine("RespawnPlayerCo");
    }

    public IEnumerator RespawnPlayerCo()
    {
        player.enabled = false;
        player.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(respawnDelay);
        player.transform.position = checkPoint.transform.position;
        player.enabled = true;
        player.GetComponent<Renderer>().enabled = true;
        Instantiate(respawnParticle, checkPoint.transform.position - Vector3.one, checkPoint.transform.rotation);

    }
}
