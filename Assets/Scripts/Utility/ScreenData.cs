using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenData : MonoBehaviour
{
    #region Fields

    static int screenWidth;
    static int screenHeight;

    static float left;
    static float right;
    static float top;
    static float bottom;

    #endregion

    #region Properties

    public static float Left => left;

    public static float Right => right;

    public static float Top => top;

    public static float Bottom => bottom;

    #endregion

    #region Methods

    public static void Initialize()
    {
        // save to support resolution changes
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        // save screen edges in world coordinates
        float screenZ = -Camera.main.transform.position.z;
        Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
        Vector3 upperRightCornerScreen = new Vector3(screenWidth, screenHeight, screenZ);
        Vector3 lowerLeftCornerWorld = Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
        Vector3 upperRightCornerWorld = Camera.main.ScreenToWorldPoint(upperRightCornerScreen);

        left = lowerLeftCornerWorld.x;
        right = upperRightCornerWorld.x;
        top = upperRightCornerWorld.y;
        bottom = lowerLeftCornerWorld.y;
    }

    public static void CheckScreenSizeChanged()
    {
        if (screenWidth != Screen.width || screenHeight != Screen.height) Initialize();
    }

    #endregion
}