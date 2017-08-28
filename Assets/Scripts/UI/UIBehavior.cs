using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour {

    public Text scoreText;
    public GameOverPanel gameOverPanel;
    public HealthBarBehaviour healthBar;
    public PausePanel pausePanel;
    public MainMenu mainMenu;
    public SkinObject[] skins;
    public ShopPanel shopPanel;

    void Awake()
    {
        Director.UI = this;
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = Director.Score.ToString();
	}

    public void OpenGameOverPanel()
    {
        gameOverPanel.Show();
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
}
