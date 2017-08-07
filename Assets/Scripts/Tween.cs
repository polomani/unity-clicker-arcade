using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tween : MonoBehaviour{

    private void setValue(Component obj, string property, float value)
    {
        obj.GetType().GetField(property).SetValue(obj, value);
    }

    private float getValue(Component obj, string property)
    {
        return (float) obj.GetType().GetField(property).GetValue(obj);
    }

    private IEnumerator CoroutineTo(Component obj, string property, float to, float duration, Action<float> OnUpdate, Action callback)
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            OnUpdate(time);
            if (time >= duration)
            {
                if (callback!=null) callback();
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator CoroutineDelay(float duration, Action callback)
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            if (time >= duration)
            {
                callback();
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public static void To(Component obj, string property, float to, float duration, Action callback)
    {
        Tween tween = Utils.GetOrAddComponent<Tween>(obj);
        tween.StartCoroutine(tween.CoroutineTo(obj, property, to, duration, 
            (time)=>tween.setValue(obj, property, Mathf.Lerp(tween.getValue(obj, property), to, time / duration)),
            callback));
    }

    public static void Delay(Component obj, float duration, Action callback)
    {
        Tween tween = Utils.GetOrAddComponent<Tween>(obj);
        tween.StartCoroutine(tween.CoroutineDelay(duration, callback));
    }
}
