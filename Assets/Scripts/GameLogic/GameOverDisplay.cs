using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverDisplay : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(ReloadScene);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ReloadScene);
    }

    public void Show()
    {
        _container.gameObject.SetActive(true);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}