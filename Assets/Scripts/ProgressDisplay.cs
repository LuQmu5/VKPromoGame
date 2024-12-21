using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressDisplay : MonoBehaviour
{
    [SerializeField] private RawImage _giftsImage;
    [SerializeField] private Image _curtainImage;

    private int _giftsCount;

    public void Init(int giftsCount)
    {
        _giftsCount = giftsCount;

        Rect rect = _giftsImage.uvRect;
        rect.width = giftsCount;
        _giftsImage.uvRect = rect;
    }

    public void UpdateView(bool isIncrease)
    {
        float multiplier = isIncrease ? -1 : 1;
        float offset = multiplier / _giftsCount;
        _curtainImage.fillAmount += offset;

        print(isIncrease);
        print(offset);
    }
}
