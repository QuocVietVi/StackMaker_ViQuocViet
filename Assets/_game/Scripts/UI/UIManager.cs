using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Button startBtn, closeBtn,retryBtn, nextLvBtn, menu;

    private void Start()
    {
        startBtn.onClick.AddListener(SpawnMap);
        retryBtn.onClick.AddListener(RetryOnClick);
        nextLvBtn.onClick.AddListener(NextLevelOnClick);
        menu.onClick.AddListener(ActiveMenu);
    }

    private void SpawnMap()
    {
        LevelManager.Instance.LoadLevel(1);

    }

    private void ActiveMenu()
    {
        LevelManager.Instance.MainMenu();
    }

    private void RetryOnClick()
    {
        LevelManager.Instance.RetryAction();
    }
    private void NextLevelOnClick()
    {
        LevelManager.Instance.NextLevelAction();
    }
}
