using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtonManager : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private LevelButtonAction levelButton;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject player;
    private void Start()
    {
        SpawnButton();
    }
    private void SpawnButton()
    {
        for (int i = 0; i < levelData.levelDataItems.Count; i++)
        {
            LevelButtonAction button = Instantiate(levelButton, this.transform);
            button.lvText.text = "Level " + levelData.levelDataItems[i].levelId.ToString();
            button.SetOnClick(levelData.levelDataItems[i].levelId);
            button.menuPanel = mainMenu;
            button.player = this.player;
        }
    }
}
