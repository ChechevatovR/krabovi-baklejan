using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AddFog : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float len;
    public float prevPosition, delta;
    void Start()
    {
        prevPosition = player.transform.position.y;
    }

    private void FixedUpdate()
    {
        float y = player.transform.position.y;
        if (y < prevPosition) RenderSettings.fogDensity += delta;
        else if (y > prevPosition && RenderSettings.fogDensity > 0.1) RenderSettings.fogDensity -= delta;
        if (y > len) RenderSettings.fogDensity = 0f;
        prevPosition = y;

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 4)
        {
            RenderSettings.fog =true;
            RenderSettings.fogDensity = 0.1f;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        Debug.Log(other.gameObject.layer);
        if (other.gameObject.layer == 4)
        {
            RenderSettings.fog = false;
        }
    }
}
