using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour {

    private SkinObject[] skins;
    public Text coins;
    public ScrollRect scrollRect;
    public float verticalNormalizedPosition;
    private bool scrolling = false;

	// Use this for initialization
	void Start () {
        skins = Director.UI.skins;
        Hide();
	}
	
	// Update is called once per frame
	void Update () {
        
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

    public void UpdateCoinsText() {
        coins.text = "$" + Repository.Data.Coins;
        Director.UI.UpdateCoinsText();
    }

    public void scrollDown()
    {
        if (!scrolling && scrollRect.verticalNormalizedPosition>0.1)
        {
            scrolling = true;
            Tween.To(scrollRect, "verticalNormalizedPosition", 
                scrollRect.verticalNormalizedPosition - 0.4f, 0.3f, false,
                () => scrolling = false);
        }
    }

    public void scrollUp()
    {
        if (!scrolling && scrollRect.verticalNormalizedPosition<0.9)
        {
            scrolling = true;
            Tween.To(scrollRect, "verticalNormalizedPosition", 
                scrollRect.verticalNormalizedPosition + 0.4f, 0.3f, false,
                () => scrolling = false);
        }
    }
}
