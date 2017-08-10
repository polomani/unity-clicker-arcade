using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour {

    public GameObject button; 

    void Start()
    {
        Hide();
    }

    void Update()
    {

    }

    public void OnRestartClick(GameObject window)
    {
        Director.Restart();
        Hide();
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
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ShowButton()
    {
        button.SetActive(true);
    }

    public void HideButton()
    {
        button.SetActive(false);
    }
}
