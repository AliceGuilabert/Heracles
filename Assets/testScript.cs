using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour {

    otherDialogue mydialog;

    private void Start()
    {
        mydialog = FindObjectOfType<otherDialogue>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            Debug.Log("Debut");
            StartCoroutine(Lion(collision.GetComponent<PlayerController>()));            
        }
    }


    IEnumerator Lion(PlayerController myPlayer)
    {
        CameraScript myCam = FindObjectOfType<CameraScript>();
        myCam.isParalax = false;
        myPlayer.pause = true;     

        while (myCam.transform.position.x < 2.91f )
        {
            myCam.transform.position = new Vector3(myCam.transform.position.x + 0.09f,
                myCam.MinPosition.y, myCam.transform.position.z);
            yield return new WaitForFixedUpdate();
        }

        mydialog.RunDialogue(7);
        yield return new WaitWhile(() => mydialog.onDialogue);

        Debug.Assert(GameObject.Find("Lion").GetComponent<Rigidbody2D>() != null, "Pas trouvé le lion");
        GameObject.Find("Lion").GetComponent<Rigidbody2D>().velocity = new Vector2(-8, 13);
        GameObject.Find("Lion").GetComponentInChildren<Animator>().SetTrigger("Attack");



    }
}
