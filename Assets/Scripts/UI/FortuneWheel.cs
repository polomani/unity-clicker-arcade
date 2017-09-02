using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FortuneWheel : MonoBehaviour {

    public Sprite normalSprite;
    public Sprite reducedSprite;
    public Image wheel;
    public Text text;
    public Button button;
    public Button homeButton;
    public Text coins;
    public int[] chance = new int[8] {8, 18, 13, 7, 18, 5, 18, 13};
    public int[] gifts = new int[8] {50, 10, 20, 100, 10, 150, 10, 20};
    public int[] giftsReduced = new int[8] { 25, 5, 10, 50, 5, 75, 5, 10 };
    private Quaternion initialRotation;
    public float rotationTo;
    private bool reduced = false; 

	void Start () {
        Hide();
        initialRotation = wheel.transform.rotation;
	}
	
	void Update () {
        wheel.transform.rotation = initialRotation * Quaternion.Euler(0, 0, rotationTo);
	}

    public void Rotate()
    {
        text.text = "";
        SetReduced();
        HideButton();
        HideHomeButton();
        int giftNumber = -1;
        int random = Random.Range(1, 100);
        int sumPercents = 0;
        while (random > sumPercents)
        {
            sumPercents += chance[++giftNumber];
        }
        rotationTo = - initialRotation.eulerAngles.z + wheel.transform.rotation.eulerAngles.z;
        Tween.To(this, "rotationTo", 360*7 + giftNumber * 360 / 8, 7f, false, 
            Transition.SMOOTH_STEP, () => Rewarding(giftNumber));
    }

    public void Rewarding(int giftNumber)
    {
        Director.UI.unixTime.Now((now) => OnRewardSuccess(giftNumber, now) , OnUnixError);
    }

    private void OnRewardSuccess(int giftNumber, int now)
    { 
        int gift = reduced ? giftsReduced[giftNumber] : gifts[giftNumber];
        text.text = "Your reward is: $" + gift;
        Repository.Data.Coins += gift;
        UpdateCoinsText();
        Director.UI.UpdateCoinsText();
        Repository.Data.LastFortuneSpin = now;
        ShowHomeButton();
        ShowButton();
        reduced = true;
    }

    public void UpdateCoinsText()
    {
        coins.text = "$" + Repository.Data.Coins;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        UpdateCoinsText();
        HideButton();
        Director.UI.unixTime.Now(OnUnixLoaded, OnUnixError);
    }

    public void SetReduced()
    {
        if (reduced)
        {
            wheel.sprite = reducedSprite;
        }
        else
        {
            wheel.sprite = normalSprite;
        }
    }

    void OnUnixLoaded(int now)
    {
        reduced = (now - Repository.Data.LastFortuneSpin) / 60 < 24;
        SetReduced();
        ShowButton();
    }

    void OnUnixError(string error)
    {
        text.text = "Forbidden";
        Debug.Log(error);
        ShowHomeButton();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowButton()
    {
        button.interactable = true;
    }

    private void HideButton()
    {
        button.interactable = false;
    }

    private void ShowHomeButton()
    {
        homeButton.interactable = true;
    }

    private void HideHomeButton()
    {
        homeButton.interactable = false;
    }
}
