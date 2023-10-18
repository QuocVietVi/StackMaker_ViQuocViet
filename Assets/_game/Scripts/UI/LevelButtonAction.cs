using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonAction : MonoBehaviour
{
    [SerializeField] private Button lvButton;
    public Text lvText;
    public GameObject menuPanel;
    public GameObject player;

    public void SetOnClick(int id)
    {
        lvButton.onClick.AddListener(() =>
        {
            SpawnMap(id);
        });
    }
    private void SpawnMap(int id)
    {
        GameObject map = Resources.Load<GameObject>(ConstantName.MAP_NAME + id);
        Instantiate(map);
        menuPanel.SetActive(false);
        Invoke(nameof(SpawnPlayer), 0.5f);
    }

    private void SpawnPlayer()
    {
        player.SetActive(true);
    }

}

