using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int speed;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private List<GameObject> brickList;
    private float horizontal, vertical;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
        {
            rb.velocity = new Vector3(horizontal * speed, 0, 0);
        }
        if (vertical != 0)
        {
            rb.velocity = new Vector3(0, 0, vertical * speed);
        }
    }
    public void CollectBrick(GameObject brick)
    {
        //transform.position = new Vector3 (transform.position.x, transform.position.y + 1f, transform.position.z);
        brick.transform.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y + 1f, spawnPoint.transform.position.z);
        brick.transform.SetParent(transform);
        brickList.Add(brick);
        for(int i = 0; i < brickList.Count; i++)
        {
            brick.name = $"Brick On Hand ({i})" ;
        }
        UpdateBrick();
    }

    private void UpdateBrick()
    {
        spawnPoint = brickList[brickList.Count - 1];
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "StopPoint")
        {
            rb.velocity = Vector3.zero;
        }
    }
}
