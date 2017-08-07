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

    private IEnumerator CoroutineTo(Component obj, string property, float to, float duration, Func<float, float> transition, Action callback)
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            setValue(obj, property, Mathf.Lerp(getValue(obj, property), to, transition(time/duration)));
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
                if (callback!=null) callback();
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public static void To(Component obj, string property, float to, float duration)
    {
        To(obj, property, to, duration, Transition.LINEAR, null);
    }

    public static void To(Component obj, string property, float to, float duration, Action callback)
    {
        To(obj, property, to, duration, Transition.LINEAR, callback);
    }

    public static void To(Component obj, string property, float to, float duration, Func<float,float> transition, Action callback)
    {
        Tween tween = Utils.GetOrAddComponent<Tween>(obj);
        tween.StartCoroutine(tween.CoroutineTo(obj, property, to, duration, 
            transition,
            callback));
    }

    public static void Delay(Component obj, float duration)
    {
        Delay(obj, duration, null);
    }

    public static void Delay(Component obj, float duration, Action callback)
    {
        Tween tween = Utils.GetOrAddComponent<Tween>(obj);
        tween.StartCoroutine(tween.CoroutineDelay(duration, callback));
    }
}

public static class Transition {

    public static float LINEAR (float t) {
        return t;
    }

    public static float EASE_IN(float t)
    {
        return 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
    }

    public static float EASE_OUT(float t)
    {
        return Mathf.Sin(t * Mathf.PI * 0.5f);
    }

    public static float SMOOTH_STEP(float t)
    {
        return t * t * (3f - 2f * t);
    }

    public static float SMOOTHER_STEP(float t)
    {
        return t*t*t * (t * (6f*t - 15f) + 10f);
    }
}
