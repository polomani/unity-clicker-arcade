using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour {

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void OnPauseClick(GameObject window)
    {      
        if (Director.Paused)
        {
            unpause();
        }
        else
        {
            pause();
        }
        window.SetActive(Director.Paused);
    }

    public void OnContinueClick(GameObject window)
    {
        unpause();
        window.SetActive(false);
    }

    void pause()
    {
        Director.Paused = true;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
        {
            go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
        }
        Time.timeScale = 0;
    }

    void unpause()
    {
        Director.Paused = false;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
        {
            go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
        }
        Time.timeScale = 1;
    }
}
