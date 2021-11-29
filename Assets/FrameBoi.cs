using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FrameBoi : EditorWindow
{
    public bool start = true;
    public int frame = 0;
    public float time = 0;

    void OnGUI()
    {
        if(WindowManager.currentFrame != null) {
            EditorGUI.DrawPreviewTexture(new Rect(0, 0, 480, 360), WindowManager.currentFrame);
        }
    }
        // Update is called once per frame
    void Update()
    {
        WindowManager.showFrame(frame);
        if (frame < 6572)
        {
            frame++;
        }
        Repaint();
    }
}
