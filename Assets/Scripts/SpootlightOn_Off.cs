using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpootlightOn_Off : MonoBehaviour
{
    Light lighter; 
    public bool lightOn = false; 

    void Awake () {
        lighter = GetComponent<Light> ();
        if(!lightOn){
            lighter.intensity = 0;
        } else {
            lighter.intensity = 1;
        }
    }

    void Update () { 
        //Заменить на кнопку на контроллере
        if (Input.GetKeyDown (KeyCode.F)) {
            if(!lightOn){ 
                lighter.intensity = 1; 
                lightOn = true; 
            } else { 
                lighter.intensity = 0; 
                lightOn = false; 
            }
        }
    }
}
