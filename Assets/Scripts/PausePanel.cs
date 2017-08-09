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

    public void OnRestartClick(GameObject window)
    {
        Director.Restart();
        window.SetActive(false);
    }

    public void OnPauseClick(GameObject window)
    {      
        if (Director.Paused)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
        window.SetActive(Director.Paused);
    }

    public void OnContinueClick(GameObject window)
    {
        Unpause();
        window.SetActive(false);
    }

    void Pause()
    {
        Director.Paused = true;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
        {
            go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
        }
        Time.timeScale = 0;
    }

    public void Unpause()
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
