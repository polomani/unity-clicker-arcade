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
        if (!Director.UI.gameOverPanel.isActiveAndEnabled)
        {
            if (Director.Paused)
            {
                Director.Unpause();
            }
            else
            {
                Director.Pause();
            }
            window.SetActive(Director.Paused);
        }
    }

    public void OnContinueClick(GameObject window)
    {
        Director.Unpause();
        window.SetActive(false);
    }


}
