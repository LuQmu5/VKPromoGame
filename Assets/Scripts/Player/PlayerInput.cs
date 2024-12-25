using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector3> HorizontalInput;

    private void Update()
    {
        Vector3 currentMousePosition = ScreenInfo.GetMouseWorldPosition();
        HorizontalInput?.Invoke(currentMousePosition);
    }
}