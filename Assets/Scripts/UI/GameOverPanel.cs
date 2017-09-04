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
            var score = Director.Score;
            scoreText.text = score.ToString();
            if (score > Repository.Data.BestResult)
            {
                scoreText.text = "[BEST] " + scoreText.text;
                Repository.Data.BestResult = score;
                Director.UI.SetBestScoreText(score);
                Director.UI.gpg.ReportScore(score);
            }
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
