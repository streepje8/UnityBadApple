using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WindowPixel : EditorWindow
{
    public int x = 0;
    public int y = 0;
    public bool state = false;
    void Update()
    {
        //Close();
        Rect r = position;
        if (state)
        {
            r.x = x * 1f;
            r.y = y * 2f;
        }
        else
        {
            r.x = 10000;
            r.y = 10000;
        }
        position = r;
    }
}
