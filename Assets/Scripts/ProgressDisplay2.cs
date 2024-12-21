using UnityEngine;
using UnityEngine.UI;

public class ProgressDisplay2 : MonoBehaviour
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
        float value = isIncrease ? 1 : -1;
        print(value);
        _currentProgress += value;
        print(_currentProgress);
        _curtainImage.fillAmount = _currentProgress / _giftsCount;
        print(_currentProgress / _giftsCount);
    }
}