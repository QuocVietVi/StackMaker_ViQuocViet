using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public enum Direct { Forward, Back, Right, Left, None}
public class Player : MonoBehaviour
{
    //[SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private List<GameObject> brickList;
    [SerializeField] private float distance;
    //[SerializeField] private Transform playerTransform;
    //[SerializeField] private float x1, x2;
    //[SerializeField] private float y1, y2;
    public LayerMask layerBrick;

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

            if (Input.GetMouseButtonDown(0))
            {
                mouseDown = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                mouseUp = Input.mousePosition;
                GetDirect(mouseDown, mouseUp);
            }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        CheckEndPoint(endPoint);

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
                    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    endPoint = Vector3.forward;
                   // CheckEndPoint(Vector3.forward);
                    //Debug.DrawRay(transform.position, Vector3.forward * distance, Color.red);
                    speed = 10;
                }
                //vuot xuong duoi
                else
                {
                    direct = Direct.Back;
                    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    endPoint= Vector3.back;
                    //CheckEndPoint(Vector3.back);
                    //Debug.DrawRay(transform.position, Vector3.back * distance, Color.red);
                    speed = 10;


                }
            }
            else
            {
                //vuot sang phai
                if(deltaX > 0)
                {
                    direct = Direct.Right;
                    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                    endPoint = Vector3.right;
                    //CheckEndPoint(Vector3.right);
                    //Debug.DrawRay(transform.position, Vector3.right * distance, Color.red);
                    speed = 10;

                }
                //vuot sang trai
                else
                {
                    direct = Direct.Left;
                    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
                    endPoint = Vector3.left;
                    //CheckEndPoint(Vector3.left);
                    //Debug.DrawRay(transform.position, Vector3.left * distance, Color.red);
                    speed = 10;
                }
            }
        }
        return direct;
    }

    private Vector3 GetNextPoint(Direct direct)
    {
        RaycastHit hit;
        Vector3 nextPoint = Vector3.zero;
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
            default:
                break;
   
        }
        for(int i = 1; i < 100; i++)
        {
            
            if (Physics.Raycast(transform.position + dir *i + new Vector3(0,6,9), Vector3.down, out hit, 10f, layerBrick))
            {
                nextPoint = hit.collider.transform.position;

            }
            else
            {
                break;

            }
        }
        return nextPoint;

    }
    private void CheckEndPoint(Vector3 vector)
    {
        Ray ray = new Ray(transform.position, vector);
        //Physics.Raycast(transform.position, Vector3.forward, distance);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, distance);
        Debug.DrawRay(transform.position, vector * distance, Color.red);

        if (hit.transform != null)
        {
            if (hit.collider.tag == "StopPoint")
            {
                Debug.Log("End Point");
                //rb.velocity = Vector3.zero;
                speed = 0;
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
        brick.transform.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y + 2f, spawnPoint.transform.position.z);
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
