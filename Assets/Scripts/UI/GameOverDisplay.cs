﻿using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverDisplay : MonoBehaviour
{
    [SerializeField] private Transform _wrapper;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _button;
    [SerializeField] private MainMenuManager _menuManager;

    private void OnEnable()
    {
        _button.onClick.AddListener(EndGame);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(EndGame);
    }

    public void Show()
    {
        _wrapper.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _wrapper.gameObject.SetActive(false);
    }

    private void EndGame()
    {
        Hide();
        _menuManager.Show();
    }
}