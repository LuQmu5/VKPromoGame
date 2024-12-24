using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressDisplayRepeatImage : ProgressDisplay
{
    [SerializeField] private RawImage _progressImage;
    [SerializeField] private Image _maskImage;

    public override void Init(int maxProgress)
    {
        MaxProgress = maxProgress;

        Rect rect = _progressImage.uvRect;
        rect.width = maxProgress;
        _progressImage.uvRect = rect;
    }

    public override void UpdateView(bool isProgressIncreased)
    {
        float multiplier = isProgressIncreased ? 1 : -1;
        float offset = multiplier / MaxProgress;
        _maskImage.fillAmount += offset;
    }
}
