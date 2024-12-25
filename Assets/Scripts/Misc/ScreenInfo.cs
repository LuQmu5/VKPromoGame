using UnityEngine;
using System;

public enum ScreenBoundary
{
    BottomLeft,
    TopLeft,
    TopRight,
    BottomRight
}

public static class ScreenInfo
{
    private static Plane _plane = new Plane(Vector3.forward, Vector3.zero);
    private static Vector2 _bottomLeftPoint = new Vector2(0, 0);
    private static Vector2 _topLeftPoint = new Vector2(0, Screen.height);
    private static Vector2 _topRightPoint = new Vector2(Screen.width, Screen.height);
    private static Vector2 _bottomRightPoint = new Vector2(Screen.width, 0);

    public static Vector3 GetWorldPosition(ScreenBoundary boundary)
    {
        switch (boundary)
        {
            case ScreenBoundary.BottomLeft:
                return CalcPosition(_bottomLeftPoint);

            case ScreenBoundary.TopLeft:
                return CalcPosition(_topLeftPoint);

            case ScreenBoundary.TopRight:
                return CalcPosition(_topRightPoint);

            case ScreenBoundary.BottomRight:
                return CalcPosition(_bottomRightPoint);

            default:
                throw new ArgumentNullException(nameof(boundary));
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 tapPosition = Input.mousePosition;
        tapPosition.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(tapPosition);

        return worldPosition;
    }

    private static Vector3 CalcPosition2(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        Vector3 pos = Vector3.zero;
        float dist = 0;

        if (_plane.Raycast(ray, out dist))
            pos = ray.GetPoint(dist);

        return pos;
    }

    private static Vector3 CalcPosition(Vector2 screenPos)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPos);
        worldPosition.z = 0;

        return worldPosition;
    }
}