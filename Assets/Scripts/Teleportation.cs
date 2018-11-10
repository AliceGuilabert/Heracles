using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleportation : MonoBehaviour {

    public bool enableOnStart;
    private bool enable;

    public Vector2 teleportPosition;
    public GameObject previousBackground;
    public GameObject nextBackground;

    public bool returnPerso;

    public Image fadeImage;
    public Animator fadeAnimator;

    public Camera Camera;
    private CameraScript myCamera;

    public Vector2 minPositionCamera;
    public Vector2 maxPositionCamera;

    private void Start()
    {
        myCamera = Camera.GetComponent<CameraScript>();
        enable = enableOnStart;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Player") && enable)
        {
            Debug.Log("telepoooortation");
            StartCoroutine(TeleportAnimation(collision.transform));
        }
    }


    IEnumerator TeleportAnimation(Transform player)
    {
        player.GetComponent<PlayerController>().pause = true;

        fadeAnimator.SetBool("Fade", true);
        yield return new WaitUntil(() => fadeImage.color.a == 1);

        if(previousBackground != null)
        {
            previousBackground.SetActive(false);
        }

        player.position = teleportPosition;

        if(returnPerso)
        {
            player.localScale = new Vector2(-player.localScale.x, player.localScale.y);
        }

        if (nextBackground != null)
        {
            nextBackground.SetActive(true);
        }

        myCamera.MinPosition = minPositionCamera;
        myCamera.MaxPosition = maxPositionCamera;

        fadeAnimator.SetBool("Fade", false);
        yield return new WaitUntil(() => fadeImage.color.a == 0);
        player.GetComponent<PlayerController>().pause = false;

    }
}
