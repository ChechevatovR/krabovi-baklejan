using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = System.Random;


public class TestGeneration : MonoBehaviour
{
   
    public GameObject pipeVer;
    public int numOfBlocks = 10, board = 10, sz = 6, maxIter = 10;
    public GameObject[] inst_pipe;
    private int startX = 0, startY = 0, startZ = 0;
	private int angleX = 0, angleY = 0, angleZ = 0;
    private int[,] moveTo = new int[6, 3] {{0, 0, 1}, {0, 1, 0}, {1, 0, 0}, {0, 0, -1}, {0, -1, 0}, {-1, 0, 0}};
    public bool[,,] used = new bool[10,10,10];
    
    Random rnd = new Random();

    void CreatePipe(int x, int y, int z, bool hor, int pos)
    {
        inst_pipe[pos] = Instantiate(pipeVer, new Vector3(x, y, z), Quaternion.identity) as GameObject;
    }
    
    void generatePipe()
    {
        //можно доделать или вообще переписать
        int prevDirection = 1;
        bool hor = false;
        for (int i = 0; i < numOfBlocks; i++)
        {
            
            //выбор позиции
            int forbidden = (prevDirection + 3) % sz;
            int step = 0;
            bool found = false;
            while(!found && step < maxIter)
            {
                int j = rnd.Next(0, 5);
                int newX = startX + moveTo[j,0];
                int newY = startY + moveTo[j,1];
                int newZ = startZ + moveTo[j,2];
                
                if (!(newX >= 0 && newX < board && newY >= 0 && newY < board && newZ >= 0 && newZ < board)) continue;
                
                if(!used[newX, newY, newZ] && j != forbidden)
                {
					
                    startX = newX;
                    startY = newY;
                    startZ = newZ;
					
					angleX = angleX + moveTo[j, 0] * 90;
					angleY = angleY + moveTo[j, 1] * 90;
					angleZ = angleZ + moveTo[j, 2] * 90;
					
					
                    if (j != prevDirection) {
						hor = !hor;
						Debug.Log("hor changed");
					}
                    prevDirection = (j + 3) % sz;
                    
                    CreatePipe(startX, startY, startZ, hor, i);
                    used[startX, startY, startZ] = true;
					inst_pipe[i].transform.Rotate(angleX, angleY, angleZ);
                    found = true;
                }

                step++;
            }
        }
    }
    void Start()
    {
        generatePipe();
        
    }
    void Update()
    {
        //update
        
    }
}
