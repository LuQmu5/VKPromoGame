using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Image _wrapper;
    [SerializeField] private float _distanceToFinish = 100;
    [SerializeField] private float _minShowTimer = 2f;

    private float _currentDistance = 0;

    public event Action TutorialFinished;

    private void Start()
    {
        _distanceToFinish = ScreenInfo.IsLandscape() ? Screen.width * 0.05f : Screen.width * 0.075f;
        print(_distanceToFinish);
        Deactivate();
    }

    public void Activate()
    {
        _wrapper.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        _wrapper.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_wrapper.gameObject.activeSelf == false)
            return;

        if (_minShowTimer > 0)
            _minShowTimer -= Time.deltaTime;

        _currentDistance += Mathf.Abs(Input.GetAxis("Mouse X"));

        if (_currentDistance >= _distanceToFinish && _minShowTimer <= 0)
        {
            _currentDistance = 0;
            TutorialFinished?.Invoke();
            _wrapper.gameObject.SetActive(false);
        }
    }
}