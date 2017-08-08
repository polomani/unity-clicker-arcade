using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tween : MonoBehaviour{

    private static GameObject holder;

    private static GameObject Holder
    {
        get {
            if (holder == null)
            {
                holder = new GameObject("TweenHolder");
                holder.AddComponent<Tween>();
            }
            return Tween.holder; 
        }
    }

    private static Tween tweenInstance;

    private static Tween TweenInstance
    {
        get
        {
            if (tweenInstance == null)
            {
                tweenInstance = Holder.GetComponent<Tween>();
            }
            return tweenInstance; 
        }
    }

    private void setValue(object obj, string property, float value)
    {
        obj.GetType().GetField(property).SetValue(obj, value);
    }

    private float getValue(object obj, string property)
    {
        return (float) obj.GetType().GetField(property).GetValue(obj);
    }

    private IEnumerator CoroutineTo(object obj, string property, float to, float duration, Func<float, float> transition, Action callback)
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

    public static void To(object obj, string property, float to, float duration)
    {
        To(obj, property, to, duration, Transition.LINEAR, null);
    }

    public static void To(object obj, string property, float to, float duration, Action callback)
    {
        To(obj, property, to, duration, Transition.LINEAR, callback);
    }

    public static void To(object obj, string property, float to, float duration, Func<float,float> transition, Action callback)
    {
        TweenInstance.StartCoroutine(TweenInstance.CoroutineTo(obj, property, to, duration, 
            transition,
            callback));
    }

    public static void Delay(object obj, float duration)
    {
        Delay(obj, duration, null);
    }

    public static void Delay(object obj, float duration, Action callback)
    {
        TweenInstance.StartCoroutine(TweenInstance.CoroutineDelay(duration, callback));
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
