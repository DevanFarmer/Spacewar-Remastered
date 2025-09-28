using System.Collections.Generic;
using UnityEngine;

public class CameraUtilities: MonoBehaviour
{
    #region Singleton
    public static CameraUtilities Instance { get; private set; }

    void HandleSingleton()
    {
        if (Instance != null)
        {
            DestroyImmediate(Instance);
            return;
        }

        Instance = this;
    }
    #endregion

    private Camera cam;
    Vector3 bottomLeft;
    Vector3 topRight;

    void Awake()
    {
        HandleSingleton();
        cam = Camera.main;
    }

    public float GetTop()
    {
        UpdateTopRight();
        return topRight.y;
    }

    public float GetBottom()
    {
        UpdateBottomLeft();
        return bottomLeft.y;
    }

    public float GetLeft()
    {
        UpdateBottomLeft();
        return bottomLeft.x;
    }

    public float GetRight()
    {
        UpdateTopRight();
        return topRight.x;
    }

    public Vector3 GetTopRight()
    {
        UpdateTopRight();
        return topRight;
    }

    public Vector3 GetBottomLeft()
    {
        UpdateBottomLeft();
        return bottomLeft;
    }

    void UpdateTopRight()
    {
        topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z - cam.transform.position.z));
    }

    void UpdateBottomLeft()
    {
        bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z - cam.transform.position.z));
    }
}
