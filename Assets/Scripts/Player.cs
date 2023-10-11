using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public enum Direct { Forward, Back, Right, Left, None}
public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int speed;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private List<GameObject> brickList;
    [SerializeField] private float distance;
    //[SerializeField] private Transform playerTransform;
    //[SerializeField] private float x1, x2;
    //[SerializeField] private float y1, y2;
    public LayerMask layerEndPoint;

    private float horizontal, vertical;
    private Vector3 mouseUp, mouseDown;
    private bool isMoving;
    private Vector3 endPoint;


    //private int move;
    private void Start()
    {
        isMoving = false;
    }

    private void Update()
    {
 
        //CheckEndPoint();
        if (!isMoving)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseDown = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                mouseUp = Input.mousePosition;
                Direct direct = GetDirect(mouseDown, mouseUp);
                if (direct != Direct.None)
                {
                    endPoint = GetEndPoint(direct);
                    isMoving = true;
                }
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, endPoint) < 0.1f)
            {
                isMoving = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);
        }
        
    }

    private Direct GetDirect(Vector3 mouseDown, Vector3 mouseUp)
    {
        Direct direct = Direct.None;

        float deltaX = mouseUp.x - mouseDown.x;
        float deltaY = mouseUp.y - mouseDown.y;

        if(Vector3.Distance(mouseDown, mouseUp) < 100)
        {
            direct = Direct.None;
        }
        else
        {
            if(Mathf.Abs(deltaY) > Mathf.Abs(deltaX))
            {
                //vuot len tren
                if (deltaY > 0)
                {
                    direct = Direct.Forward;
                    transform.position = Vector3.MoveTowards(transform.position, mouseDown, 0);
                }
                //vuot xuong duoi
                else
                {
                    direct = Direct.Back;
                }
            }
            else
            {
                //vuot sang phai
                if(deltaX > 0)
                {
                    direct = Direct.Right;
                }
                //vuot sang trai
                else
                {
                    direct = Direct.Left;
                }
            }
        }
        return direct;
    }

    private Vector3 GetEndPoint(Direct direct)
    {
        RaycastHit hit;
        Vector3 endPoint = Vector3.zero;
        Vector3 dir = Vector3.zero;

        switch (direct)
        {
            case Direct.Forward:
                dir = Vector3.forward;
                break;
            case Direct.Back:
                dir = Vector3.back;
                break;
            case Direct.Right:
                dir = Vector3.right;

                break;
            case Direct.Left:
                dir = Vector3.left;

                break;
            case Direct.None:
                break;
   
        }
        for(int i = 1; i < 100; i++)
        {

            if (Physics.Raycast(transform.position + dir *i, Vector3.forward, out hit, distance, layerEndPoint))
            {
                endPoint = hit.collider.transform.position;

            }
            else
            {
                break;

            }
        }
        return endPoint;

    }
    private void CheckEndPoint()
    {
        Ray ray = new Ray(transform.position, Vector3.forward);
        //Physics.Raycast(transform.position, Vector3.forward, distance);
        RaycastHit hit;
        Physics.Raycast(ray, out hit,distance);
        Debug.DrawRay(transform.position, Vector3.forward * distance, Color.red);
        if (hit.transform != null)
        {
            if(hit.collider.tag == "StopPoint")
            {
                Debug.Log("End Point");
                //rb.velocity = Vector3.zero;
            }
        }
    }
    private void Move()
    {
        //horizontal = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw("Vertical");

        //if (horizontal != 0)
        //{
        //    rb.velocity = new Vector3(horizontal * speed, 0, 0);
        //}
        //if (vertical != 0)
        //{
        //    rb.velocity = new Vector3(0, 0, vertical * speed);
        //}
       
    }

   


    public void CollectBrick(GameObject brick)
    {
        //transform.position = new Vector3 (transform.position.x, transform.position.y + 1f, transform.position.z);
        brick.transform.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y + 1.2f, spawnPoint.transform.position.z);
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
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "StopPoint")
    //    {
    //        rb.velocity = Vector3.zero;
    //    }
    //}
}
