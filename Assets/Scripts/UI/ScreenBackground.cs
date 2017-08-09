using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBackground : MonoBehaviour
{

    public Texture2D texture;
    public GUIStyle style;
    public float alpha = 0;


    void OnGUI()
    {
        Color c = GUI.color;
        c.a = alpha;
        GUI.color = c;
        Vector2 size = new Vector2(Screen.width, Screen.height);
        GUI.BeginGroup(new Rect(0, 0, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), GUIContent.none, style);
        GUI.EndGroup();
    }

    private void Appear()
    {
        Director.WindowOpened = true;
        Tween.To(this, "alpha", 1f, 0.5f,
            () => Tween.Delay(
                this, 1, () => Tween.To(
                    this, "alpha", 0f, 0.5f, () => Director.WindowOpened = false
                )
            )
        );
    }

    void Start()
    {
        texture.wrapMode = TextureWrapMode.Repeat;
        style.normal.background = texture;
        Appear();
    }

    void Update()
    {

    }
}
