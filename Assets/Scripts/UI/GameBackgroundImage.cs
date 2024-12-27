using UnityEngine;
using UnityEngine.UI;

public class GameBackgroundImage : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _landscapeBackground;
    [SerializeField] private Sprite _portraitBackground;

    private void Start()
    {
        _image.sprite = ScreenInfo.IsLandscape()? _landscapeBackground : _portraitBackground;
    }
}
