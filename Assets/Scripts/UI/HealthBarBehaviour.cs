using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HealthBarBehaviour : MonoBehaviour {

    private Vector2 pos;
    private Vector2 size = new Vector2 (200, 10);
    public Texture2D[] textures;
    private GUIStyle[] styles;
    private float[] barsLen;

    void Awake()
    {
        styles = new GUIStyle[textures.Length];
        barsLen = new float[textures.Length];
        int w = Screen.width;
        size = new Vector2(w * 0.8f, 10);
        pos = new Vector2(w - size.x, Screen.width/3.5f) / 2;
        for (int i = 0; i < textures.Length; ++i)
        {
            setTexture(textures[i], styles[i] = new GUIStyle());
        }
        Hide();
    }

    void setTexture(Texture2D tex, GUIStyle style)
    {
        tex.wrapMode = TextureWrapMode.Repeat;
        style.normal.background = tex;
    }

	void OnGUI () {
        transform.SetAsFirstSibling(); 
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), GUIContent.none, styles[0]);

        int nLines = (int) Math.Ceiling (BossBehavior.totalHP/ 100);
        for (int i = 0; i < nLines; ++i) {
            int k = 1 + i % textures.Length;
            float barLen = (BossBehavior.HP - 100f * i) / 100f;
            if (barLen > 1) barLen = 1;
            barsLen[i] = Mathf.MoveTowards(barsLen[i], barLen, 0.5f*Time.deltaTime);
            GUI.BeginGroup(new Rect(0, 0, size.x * barsLen[i], size.y));
            GUI.Box(new Rect(0, 0, size.x, size.y), GUIContent.none, styles[k]);
            GUI.EndGroup();
        }

        GUI.EndGroup();
	}

    public void Show()
    {
        enabled = true;
    }

    public void Hide()
    {
        enabled = false;
    }
}
