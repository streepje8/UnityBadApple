using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static WindowPixel[][] windows = new WindowPixel[height][];
    public static Texture2D[] typedTextures = new Texture2D[1];
    public static int width = 60;
    public static int height = 20;
    public static Texture2D currentFrame = null;

    [MenuItem("badApple/start")]
    static void Init()
    {
        windows = new WindowPixel[height][];
        for (int y = 0; y < height; y++) { 
            WindowPixel[] row = new WindowPixel[width];
            for(int x = 0; x < width; x++)
            {
                WindowPixel window = (WindowPixel)EditorWindow.CreateInstance<WindowPixel>();
                window.position = new Rect(x * 40, y * 40, 0, 0);
                window.maxSize = new Vector2(20,20);
                window.minSize = window.maxSize;
                window.x = x * 20;
                window.y = y * 20;
                window.state = true;
                window.Show();
                row[x] = window;
            }
            windows[y] = row;
        }
        Debug.Log("Loading frames");
        System.Object[] textures = new System.Object[7777];
        textures = Resources.LoadAll("Frames",typeof(Texture2D));
        typedTextures = new Texture2D[textures.Length];
        for (int i = 0; i < textures.Length; i++)
        {
            typedTextures[i] = (Texture2D)textures[i];
        }
        Array.Sort(typedTextures, new comparerBoi());
        Debug.Log("Textures Loaded: " + typedTextures.Length);
        Debug.Log("Image Path: " + "Assets/Frames");
    }

    public static string toNumb(int i)
    {
        if(i < 10)
        {
            return "00" + i;
        }
        if(i < 100)
        {
            return "0" + i;
        }
        return i.ToString();
    }

    [MenuItem("badApple/frameboi")]
    static void Frameboi()
    {
        FrameBoi meh = (FrameBoi)EditorWindow.GetWindow(typeof(FrameBoi));
        meh.frame = 0;
        meh.Show();
    }

        [MenuItem("badApple/stop")]
    static void Stop()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                windows[y][x].Close();
            }
        }
    }

    // Update is called once per frame
    public static void showFrame(int frame)
    {
        currentFrame = typedTextures[frame];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int px = Mathf.RoundToInt(x/((float)width) * 512f);
                int py = -Mathf.RoundToInt(y/((float)height) * 265f);
                windows[y][x].state = currentFrame.GetPixel(px, py).r < 0.5f;
            }
        }
    }
}

public class comparerBoi : IComparer
{
    public int Compare(object x, object y)
    {
        Texture2D a = (Texture2D)x;
        Texture2D b = (Texture2D)y;
        return int.Parse(a.name.Substring(3)) - int.Parse(b.name.Substring(3));
    }
}
