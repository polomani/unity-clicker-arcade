using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinPanelItem : MonoBehaviour {

    private ShopPanel shop;
    public SkinObject data;
    public int skinId;
    public Text price;
    public Text text;
    public GameObject locked;
    public Button button;
    public GameObject flag;

	// Use this for initialization
	void Start () {
		shop = GetComponentInParent<ShopPanel>();
        data = Director.UI.skins[skinId];
        price.text = data.price.ToString();
        text.text = data.skinName;
        UpdateState();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Buy()
    {
        
        Repository.Data.Coins -= data.price;
        Repository.Data.setSkin(skinId, true);
        Choose();
    }

    public void Choose()
    {
        if (Repository.Data.getSkin(skinId))
        {
            Repository.Data.HeroSkin = skinId;
            Director.Hero.ActivateSkin(skinId);
            shop.gameObject.BroadcastMessage("UpdateState");
        }
    }

    public void UpdateState()
    {
        bool bought = Repository.Data.getSkin(skinId);
        shop.UpdateCoinsText();
        locked.SetActive(!bought);
        if (data.price > Repository.Data.Coins)
            button.interactable = false;
        button.gameObject.SetActive(!bought);
        flag.SetActive(Repository.Data.HeroSkin==skinId);
    } 
}
