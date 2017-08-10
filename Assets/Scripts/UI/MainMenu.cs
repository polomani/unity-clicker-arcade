using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	void Start () {
        Home();
	}
	
	public void Home () {
        Director.Restart();
        Director.Pause();
        Director.UI.HideScoreTextAndPauseButton();
        Show();
	}

    public void OnStartClick()
    {
        Hide();
        Director.UI.ShowScoreTextAndPauseButton();
        Director.Restart();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
