using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour {

    otherDialogue mydialog;
    Teleportation myTeleport;

    private void Start()
    {
        mydialog = FindObjectOfType<otherDialogue>();
        myTeleport = GetComponent<Teleportation>();
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

        //yield return new WaitForSeconds(GameObject.Find("Lion").GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(2f);

        myPlayer.GetComponent<PlayerHealthManager>().RemoveLife(myPlayer.GetComponent<PlayerHealthManager>().currentHealth - 1);
        myPlayer.manual = true;
        yield return new WaitForSeconds(1f);
        myPlayer.transform.localScale = new Vector2 (-myPlayer.transform.localScale.x, myPlayer.transform.localScale.y);
        myPlayer.myRigid.velocity = new Vector2(-18, 25);
        myPlayer.myAnim.Play("Jump Up");
        //myPlayer.myAnim.SetFloat("Jump", myPlayer.myRigid.velocity.y);        

        yield return new WaitForSeconds(2);

        StartCoroutine(myTeleport.TeleportAnimation(myPlayer.transform));
        yield return new WaitWhile(() => myTeleport.changing);
        myCam.isParalax = true;
        myPlayer.myAnim.Play("Idle");
        myPlayer.manual = false;

        yield return new WaitWhile(() => myTeleport.hasBegan);        

        mydialog.RunDialogue(8);
        yield return new WaitWhile(() => mydialog.onDialogue);

    }
}
