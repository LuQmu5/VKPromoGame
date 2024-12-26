using UnityEngine;
using UnityEngine.UI;

public class ProgressDisplayFillImage : ProgressDisplay
{
    [SerializeField] private Image _fillingImage;

    private float _currentProgress = 0;

    public override void Init(int maxProgress)
    {
        MaxProgress = maxProgress;
    }

    public override void UpdateView(bool isProgressIncreased)
    {
        _currentProgress = isProgressIncreased ? _currentProgress + 1 : _currentProgress - 1;
        _currentProgress = Mathf.Clamp(_currentProgress, 0, MaxProgress);

        _fillingImage.fillAmount = _currentProgress / MaxProgress;
    }
}
