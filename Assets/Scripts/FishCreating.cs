using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishCreating : MonoBehaviour
{
    public float speed, posX, posY, posZ, angle ;
    // Start is called before the first frame update
    void Start()
    {
        posX = Random.Range(-11f, 11f);
        posZ = Random.Range(-10f, 10f);
        posY = Random.Range(-11f, 11f);
        angle = Random.Range(-90f, 89f);
		speed = Random.Range(0.01f, 0.03f);
        transform.Rotate(new Vector3(0,angle,0), Space.World);
        transform.position = (new Vector3(posX,posY,posZ));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(0.01f, 0, 0);
    }
}