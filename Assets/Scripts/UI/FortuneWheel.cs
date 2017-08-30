using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FortuneWheel : MonoBehaviour {

    public Image wheel;
    public Text text;
    public Button button;
    public Text coins;
    public int[] chance = new int[8] {8, 18, 13, 7, 18, 5, 18, 13};
    public int[] gifts = new int[8] {50, 10, 20, 100, 10, 150, 10, 20};
    private Quaternion initialRotation;
    public float rotationTo;

	// Use this for initialization
	void Start () {
        Hide();
        initialRotation = wheel.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        wheel.transform.rotation = initialRotation * Quaternion.Euler(0, 0, rotationTo);
	}

    public void Rotate()
    {
        text.text = "";
        button.interactable = false;
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
        button.interactable = true;
        text.text = "Your reward is: $" + gifts[giftNumber];
        Repository.Data.Coins += gifts[giftNumber];
        UpdateCoinsText();
    }

    public void UpdateCoinsText()
    {
        coins.text = "$" + Repository.Data.Coins;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        UpdateCoinsText();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
