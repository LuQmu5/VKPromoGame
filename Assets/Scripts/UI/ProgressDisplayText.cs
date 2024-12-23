using TMPro;
using UnityEngine;

public class ProgressDisplayText : ProgressDisplay
{
    [SerializeField] private TMP_Text _text;

    private int _currentProgress;

    public override void Init(int maxProgress)
    {
        MaxProgress = maxProgress;
        _currentProgress = 0;
        _text.text = $"{_currentProgress}/{MaxProgress}";
    }

    public override void UpdateView(bool isProgressIncreased)
    {
        _currentProgress = isProgressIncreased ? _currentProgress + 1 : _currentProgress - 1;
        _currentProgress = Mathf.Clamp(_currentProgress, 0, MaxProgress);

        _text.text = $"{_currentProgress}/{MaxProgress}";
    }
}