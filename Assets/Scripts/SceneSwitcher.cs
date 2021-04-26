using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class SceneSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) //change to A
        {
            SceneManager.LoadSceneAsync("SampleScene (2)");
            SceneManager.UnloadSceneAsync("MainMenu");
        }
        
        if (Input.GetKeyDown(KeyCode.B)) //change to B
        {
            SceneManager.UnloadSceneAsync("MainMenu");
            Application.Quit();
        }
    }
}
