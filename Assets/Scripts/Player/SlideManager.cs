using System;
using UnityEngine;

public class SlideManager : MonoBehaviour
{
    private Vector3 _previousTapPosition;

    public event Action<float> Slide;

    private void Start()
    {
        _previousTapPosition = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 currentTapPosition = ScreenInfo.GetTapWorldPosition();
            float delta =  currentTapPosition.x - _previousTapPosition.x;
            Slide?.Invoke(delta);
            _previousTapPosition = currentTapPosition;
        }
    }
}