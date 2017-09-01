using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour {

    public void Hit()
    {
        Destroy(gameObject);
        Repository.Data.Coins += 1;
        Director.UI.UpdateCoinsText();
    }

}
