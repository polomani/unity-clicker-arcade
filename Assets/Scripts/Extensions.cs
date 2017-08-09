using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions {

    public static T GetOrAddComponent<T>(this Component component) where T : Component
    {
        var newOrExistingComponent = component.gameObject.GetComponent<T>() ?? component.gameObject.AddComponent<T>();
        return newOrExistingComponent;
    }

    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        var newOrExistingComponent = gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
        return newOrExistingComponent;
    }
}
