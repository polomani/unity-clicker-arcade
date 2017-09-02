using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class UnixTime : MonoBehaviour {

    public string server = "http://currentmillis.com/time/minutes-since-unix-epoch.php";

    public void Now(Action<int> success, Action<string> error)
    {
        StartCoroutine(GetUnixMinutes(success, error));
    }

    IEnumerator GetUnixMinutes(Action<int> success, Action<string> error)
    {
        UnityWebRequest request = UnityWebRequest.Get(server);
        yield return request.Send();

        if (request.isError)
        {
            error(request.error);
        }
        else
        {
            success(Convert.ToInt32(request.downloadHandler.text));
        }
    }
}
