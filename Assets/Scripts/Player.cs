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
    //[SerializeField] private Transform playerTransform;
    //[SerializeField] private float x1, x2;
    //[SerializeField] private float y1, y2;


    private float horizontal, vertical;
    //private int move;

    private void Update()
    {

    }


    //private void Move()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        x1 = Input.mousePosition.x;
    //        y1 = Input.mousePosition.y;
    //    }
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        x2 = Input.mousePosition.x;
    //        y2 = Input.mousePosition.y;
    //        if (x1 > x2)
    //        {
    //            Debug.Log("Move Left");
    //            move = 1;
    //        }
    //        if (x2 > x1)
    //        {
    //            Debug.Log("Move Right");
    //            move = 2;
    //        }
    //        if (y1 > y2)
    //        {
    //            Debug.Log("Move Back");
    //            move = 3;
    //        }
    //        if (y2 > y1)
    //        {
    //            Debug.Log("Move Straight");
    //            move = 4;
    //        }
    //    }

    //    if (move == 1)
    //    {
    //        rb.velocity = Vector3.left * speed;
    //    }
    //    if (move == 2)
    //    {
    //        rb.velocity = Vector3.right * speed;

    //    }
    //    if (move == 3)
    //    {
    //        rb.velocity = Vector3.back * speed;
    //    }
    //    if (move == 4)
    //    {
    //        rb.velocity = Vector3.forward * speed;
    //    }
    //}

    //private void Move()
    //{
    //    horizontal = Input.GetAxisRaw("Horizontal");
    //    vertical = Input.GetAxisRaw("Vertical");

    //    if (horizontal != 0)
    //    {
    //        rb.velocity = new Vector3(horizontal * speed, 0, 0);
    //    }
    //    if (vertical != 0)
    //    {
    //        rb.velocity = new Vector3(0, 0, vertical * speed);
    //    }
    //}

    
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
