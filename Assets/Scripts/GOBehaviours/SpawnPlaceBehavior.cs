using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlaceBehavior : MonoBehaviour {

    public SpawnType spawnType;
    public GameObject[] nearestSpawnPlaces;
}

public enum SpawnType
{
    TOPSIDE, TOP, DOWN, CENTER, TOPCENTER
}
