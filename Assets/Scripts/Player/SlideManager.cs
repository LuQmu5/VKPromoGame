using System;
using UnityEngine;

public class SlideManager : MonoBehaviour
{
    public event Action<Vector3> Slide;
    public event Action SlideEnd;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Slide?.Invoke(ScreenInfo.GetTapWorldPosition());
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            SlideEnd?.Invoke();
        }
    }
}