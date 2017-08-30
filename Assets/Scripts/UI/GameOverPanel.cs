using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour {

    public Text scoreText;

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnRestartClick(GameObject window)
    {
        Director.Restart();
        window.SetActive(false);
    }

    public void OnHomeClick()
    {
        Hide();
        Director.UI.mainMenu.Home();
    }

    public void Show()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            scoreText.text = Director.Score.ToString();
            if (Director.Score > Repository.Data.BestResult)
            {
                scoreText.text = "[BEST] " + scoreText.text;
                Repository.Data.BestResult = Director.Score;
                Director.UI.SetBestScoreText(Director.Score);
            }
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
