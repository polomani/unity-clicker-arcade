using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour {

    public GameObject coinPrefab;
    public int interval = 1;
    public float chance = 0.05f;

	void Start () {
        StartCoroutine(SpawnCoin());
	}

    IEnumerator SpawnCoin()
    {
        while (true) {
            yield return new WaitForSeconds(interval);
            if (Random.value < chance)
            {
                int x = Random.Range(0, Screen.width);
                int y = Random.Range(0, Screen.height);
                Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(x, y, Camera.main.nearClipPlane + 6));
                position.z = 0;
                Instantiate(coinPrefab, position, Quaternion.Euler(90, 0, 0));
            }
        }
    }
}
