using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private Player player;
    public GameObject brick;
    public Collider collider;
    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.CollectBrick(brick);
        }
    }
}
