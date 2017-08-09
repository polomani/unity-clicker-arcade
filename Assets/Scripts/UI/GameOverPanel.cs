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

    public void Open()
    {
        scoreText.text = Director.Score.ToString();
        gameObject.SetActive(true);
    }
}
