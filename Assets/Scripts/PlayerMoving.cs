using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
// using Valve;

public class PlayerMoving : MonoBehaviour
{ 
    [SerializeField] GameObject headset;
    [SerializeField] GameObject player;
    public float speed = 10f, rotatingSpeed = 5f, depthSpeed = 10f;
    
    
    // private Valve.VR.InteractionSystem.Hand hand;
  //  public Steamvr_i leftHand, rightHand;
    
    
    
    
    void FixedUpdate()
    {
        float for_back = Input.GetAxis("Vertical");
        float lef_rig = Input.GetAxis("Horizontal");
        float up_dn = Input.GetAxis("Depth");

        Quaternion cur_dir = headset.transform.rotation;
        
        player.transform.Translate(Vector3.forward * (for_back * speed));
        player.transform.Translate(Vector3.up * (up_dn * depthSpeed));
        player.transform.Rotate(Vector3.up * (rotatingSpeed * lef_rig));
        
    }
}
