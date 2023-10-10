using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int speed;


    private float horizontal, vertical;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (horizontal != 0)
        {
            rb.velocity = new Vector3(horizontal * speed, 0, 0);
        }
        if (vertical != 0)
        {
            rb.velocity = new Vector3(0, 0, vertical * speed);
        }
    }
}
