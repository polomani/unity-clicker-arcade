using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour {

    public Text coinsText;
    public Text scoreText;
    public Text bestScoreText;
    public GameOverPanel gameOverPanel;
    public HealthBarBehaviour healthBar;
    public PausePanel pausePanel;
    public MainMenu mainMenu;
    public SkinObject[] skins;
    public ShopPanel shopPanel;
    public UnixTime unixTime;

    void Awake()
    {
        Director.UI = this;
        Director.UI.SetBestScoreText(Repository.Data.BestResult);
        UpdateCoinsText();
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = Director.Score.ToString();
	}

    public void HideScoreTextAndPauseButton() {
        scoreText.enabled = false;
        pausePanel.HideButton();
    }

    public void ShowScoreTextAndPauseButton()
    {
        scoreText.enabled = true;
        pausePanel.ShowButton();
    }

    public void SetBestScoreText(int score)
    {
        bestScoreText.text = "Best: " + score;
    }

    public void UpdateCoinsText()
    {
        coinsText.text = "$" + Repository.Data.Coins;
    }
}
