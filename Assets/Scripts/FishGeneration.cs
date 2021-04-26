using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

public class FishGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fish;
    public GameObject[] inst_fish;

    void createFish(int x)
    {
        inst_fish[x] = Instantiate(fish,Vector3.zero, Quaternion.identity) as GameObject;

    }
    void Start()
    {
        
        for( int i = 0; i < 10; i++)
        {
            createFish(i);
        }
    }
    
    void Update()
    {
        for (int i = 0; i < 10; i++)
        {
             float curX = inst_fish[i].transform.position.x;
             float curY = inst_fish[i].transform.position.y;
             float curZ = inst_fish[i].transform.position.z;
             if (Math.Abs(curX) > 10f || Math.Abs(curY) > 10f || Math.Abs(curZ)> 10)
             {
                 Destroy(inst_fish[i]);
                 createFish(i);
             }
             
        }
    }
}
