using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private GameObject currentLevel;
    [SerializeField] private Player player;
    [SerializeField] private GameObject mainMenuPanel, victoryPanel, settingPanel;


    private int level = 1;
    public void LoadLevel(int id)
    {
        currentLevel = Instantiate(Resources.Load<GameObject>(ConstantName.MAP_NAME + id));
        mainMenuPanel.SetActive(false);
        victoryPanel.SetActive(false);
        settingPanel.SetActive(false);
        Invoke(nameof(SpawnPLayer), 0.5f);
    }

    public void RetryAction()
    {
        if(currentLevel != null) 
        {
            Destroy(currentLevel);
        }
        LoadLevel(level);
    }

    public void NextLevelAction()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
        level++;
        LoadLevel(level);
    }

    public void MainMenu()
    {
        mainMenuPanel.SetActive(true);
    }
    private void SpawnPLayer()
    {
        player.gameObject.SetActive(true);

    }
}

public static class ConstantName
{
    public static string MAP_NAME = "Map";
}