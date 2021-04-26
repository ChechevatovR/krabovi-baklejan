using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public GameObject sphereParent;
    public Text canv; 

    private int NumberofHalls;
    // Start is called before the first frame update
    void Start()
    {
        canv.text = "OO: " + NumberofHalls.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("AAAAAAAAAAAAAAA");
        NumberofHalls = sphereParent.transform.childCount;
        canv.text = "Осталось утечек: " + NumberofHalls.ToString();
    }
}
