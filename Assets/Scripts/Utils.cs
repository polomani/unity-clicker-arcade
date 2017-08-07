using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {

    public static T GetOrAddComponent<T>(this Component component) where T : Component
    {
        var newOrExistingComponent = component.gameObject.GetComponent<T>() ?? component.gameObject.AddComponent<T>();
        return newOrExistingComponent;
    }
}
