using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using System;

public class MinimapDinamicIcons : MonoBehaviour

{
    public GameObject spheresParent;

    public GameObject player;

    public float deltaHeight = 1f;
    public GameObject minimapSphere, arrowUp, arrowDown;
    private Mesh upArr, downArr, sphereMesh;

    void Start()
    {
        upArr = arrowUp.GetComponentInChildren<MeshFilter>().mesh;
        downArr = arrowDown.GetComponentInChildren<MeshFilter>().mesh;
        sphereMesh = minimapSphere.GetComponent<MeshFilter>().mesh;
    }
    void Update()
    //comment
    {
        foreach(Transform child in spheresParent.transform)
        {
            // GameObject curSphere = child.gameObject;
            GameObject curSphere = child.gameObject;
            float sphereHeight = curSphere.transform.position.y;
            float playerHeight = player.transform.position.y;
            Debug.Log((sphereHeight - playerHeight));
            float curDelta = sphereHeight - playerHeight;
            if (curDelta > deltaHeight)
            {
                curSphere.GetComponent<MeshFilter>().mesh = upArr;
                curSphere.transform.rotation = Quaternion.Euler(-90, -90 +  90 * (curDelta / Math.Abs(curDelta)), 0);
            }

            else if (curDelta  < -deltaHeight)
            {
                curSphere.GetComponent<MeshFilter>().mesh = upArr;
                curSphere.transform.rotation = Quaternion.Euler(-90, -90 +  90 * (curDelta / Math.Abs(curDelta)), 0);
            }

            else if (Math.Abs(curDelta) <= deltaHeight)
            {
                curSphere.GetComponent<MeshFilter>().mesh = sphereMesh;
            }
        }
    }
}
