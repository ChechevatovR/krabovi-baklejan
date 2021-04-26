using System;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class BatteryScript : MonoBehaviour
    {
        public GameObject light;
        
        public float discharging = 1f, charging = 10f; // Units per second
        public float capacity = 100; // Battery capacity units
        private int time_before_death;
        public float depth = 6.5f; // Water level
        public GameObject player;
        public float cur_charge;
        private bool gameover = false;
        
        private int deltacnt = 350;
        public GameObject LoseMessage;
        
        public Text butteryText;
            
        private void Start()
        {
            LoseMessage.SetActive(false);
            time_before_death = (int)Math.Ceiling(capacity / discharging);
            cur_charge = capacity;
        }

        private void FixedUpdate()
        {
            if (gameover)
            {
                deltacnt--;
                if (deltacnt < 0)
                {
                    SceneManager.UnloadSceneAsync("SampleScene (2)");
                    SceneManager.LoadScene("Scenes/MainMenu");
                }
                return;
            }

            float y = player.transform.position.y;
            
            if (y > depth && cur_charge < capacity) // Charging
            {
                cur_charge += charging * Time.deltaTime / 4;
            }

            if (y < depth && cur_charge >= 0);
            {
                if (light.GetComponent<Light>().intensity == 0) cur_charge -= discharging * Time.deltaTime;
                else cur_charge -= discharging * Time.deltaTime;
            }
            
            if (cur_charge < 0)
            {
                gameover = true; butteryText.text = "";
                // GameObject a =Instantiate(LoseMessage,  Vector3.one,  Quaternion.identity);
                LoseMessage.SetActive(true);
            }
            
            butteryText.text = "Заряд батареи " + ((int)cur_charge).ToString() + " % ";
        }
    }
}