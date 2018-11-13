using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleportation : MonoBehaviour {

    public bool enableOnStart;
    public bool enable { get; set; }

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

    public bool hasBegan { get; set; }
    public bool changing { get; set; }

    private void Start()
    {
        myCamera = Camera.GetComponent<CameraScript>();
        enable = enableOnStart;
        hasBegan = false;
        changing = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Player") && enable)
        {
            Debug.Log("telepoooortation");
            StartCoroutine(TeleportAnimation(collision.transform));
        }
    }


    public IEnumerator TeleportAnimation(Transform player)
    {
        hasBegan = true;
        changing = true;
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
        changing = false;

        fadeAnimator.SetBool("Fade", false);

        yield return new WaitUntil(() => fadeImage.color.a == 0);
        player.GetComponent<PlayerController>().pause = false;
        hasBegan = false;
    }
}
