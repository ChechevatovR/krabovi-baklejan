using System;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace DefaultNamespace
{
    public class PipeRepairer : MonoBehaviour
    {
        private int score;
        public GameObject sphereParent; 
        private int deltacnt = 350;
        public GameObject WinMessage; 
        private bool isWin = false;
        private bool First = true;
        
        private void OnTriggerEnter(Collider other)
        {
            Destroy(other);
            score--;
            
            
        }

        void Start()
        {
            WinMessage.SetActive(false);
            
            Debug.Log("gdsgfjagfkj " + score);
        }


        private void FixedUpdate()
        {
            score = 100;
            if (First)
            {
                First = false;
                score = sphereParent.transform.childCount;
            }
            if (score <= 0)
            {
                isWin = true;
                WinMessage.SetActive(true);
            }
            
            if(isWin)
            {
                deltacnt--;
                if (deltacnt < 0)
                {
                    SceneManager.UnloadSceneAsync("SampleScene (2)");
                    SceneManager.LoadScene("Scenes/MainMenu");
                }
                return;
            }
        }
    }
}