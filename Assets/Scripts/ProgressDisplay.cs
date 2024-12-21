using UnityEngine;
using UnityEngine.UI;

public class ProgressDisplay : MonoBehaviour
{
    [SerializeField] private Image _curtainImage;

    private float _giftsCount;
    private float _currentProgress = 0;

    public void Init(int giftsCount)
    {
        _giftsCount = giftsCount;
    }

    public void UpdateView(bool isIncrease)
    {
        _currentProgress = isIncrease ? _currentProgress + 1 : _currentProgress - 1;
        _currentProgress = Mathf.Clamp(_currentProgress, 0, _giftsCount);

        _curtainImage.fillAmount = _currentProgress / _giftsCount;
    }
}