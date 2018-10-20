using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {

    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing;
    private Vector3 previousCameraPosition;

    public float minPositionX;
    public float maxPositionX;
    public float minPositionY;
   // public float maxPositionY;
    private float nextPositionCameraX;
    private float nextPositionCameraY;

    public GameObject player;
    private Vector3 offset;

    // Use this for initialization
    void Start () {

        offset = transform.position - player.transform.position;
        previousCameraPosition = transform.position;
        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < parallaxScales.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
	}
	
	// Update is called once per frame
	void LateUpdate () {

        if(player.transform.position.x + offset.x < minPositionX)
        {
            nextPositionCameraX = minPositionX;
        }
        else if (player.transform.position.x + offset.x > maxPositionX)
        {
            nextPositionCameraX = maxPositionX;
        } else
        {
            nextPositionCameraX = player.transform.position.x + offset.x;
        }

        if(player.transform.position.y + offset.y < minPositionY)
        {
            nextPositionCameraY = minPositionY;
        } else
        {
            nextPositionCameraY = player.transform.position.y + offset.y;
        }
        transform.position = new Vector3(nextPositionCameraX, nextPositionCameraY, transform.position.z);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            Vector3 parallax = new Vector3((previousCameraPosition.x - transform.position.x) * (parallaxScales[i] / smoothing),
                transform.position.y - previousCameraPosition.y,
            previousCameraPosition.z - transform.position.z);
            backgrounds[i].position = new Vector3(backgrounds[i].position.x + parallax.x,
                backgrounds[i].position.y + parallax.y, backgrounds[i].position.z);
   
        }
        previousCameraPosition = transform.position;
	}
}
