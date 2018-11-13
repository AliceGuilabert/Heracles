using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public bool isParalax { get; set; }

    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing;
    private Vector3 previousCameraPosition;

    public Vector2 MinPosition { get; set; }
    public Vector2 MaxPosition { get; set; }


    //TODO --> Mettre le chargement du niveau dans le Level1;

    public float minPositionX;
    public float maxPositionX;
    public float minPositionY;
    // public float maxPositionY;

    private float nextPositionCameraX;
    private float nextPositionCameraY;

    public GameObject player;
    public Vector3 offset { get; set; }

    // Use this for initialization
    void Start()
    {
        isParalax = true;

        offset = transform.position - player.transform.position;
        previousCameraPosition = transform.position;
        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < parallaxScales.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }

        /// A MODIFIER !
        MinPosition = new Vector2(minPositionX, minPositionY);
        MaxPosition = new Vector2(maxPositionX, 0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isParalax)
        {
            if (player.transform.position.x + offset.x < MinPosition.x)
            {
                nextPositionCameraX = MinPosition.x;
            }
            else if (player.transform.position.x + offset.x > MaxPosition.x)
            {
                nextPositionCameraX = MaxPosition.x;
            }
            else
            {
                nextPositionCameraX = player.transform.position.x + offset.x;
            }

            if (player.transform.position.y + offset.y < MinPosition.y)
            {
                nextPositionCameraY = MinPosition.y;
            }
            else
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
}
