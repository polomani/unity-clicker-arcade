using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DataObject {

    private int coins = 200;
    private int bestResult = 0;

    private int heroSkin = 0;
    private bool[] skins = new bool[6] { true, false, false, false, false, false };

    public int BestResult
    {
        get { return bestResult; }
        set { bestResult = value; Repository.Save(); }
    }
    public void setSkin(int n, bool bought) { skins[n] = bought; Repository.Save(); }
    public bool getSkin(int n) { return skins[n]; }
    public bool[] Skins
    {
        get { return skins; }
        set { skins = value; Repository.Save(); }
    }
    public int HeroSkin
    {
        get { return heroSkin; }
        set { heroSkin = value; Repository.Save(); }
    }
    public int Coins
    {
        get { return coins; }
        set { coins = value; Repository.Save(); }
    }
}
