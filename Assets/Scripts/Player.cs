using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public enum Direct { Forward, Back, Right, Left, None}
public class Player : MonoBehaviour
{
    //[SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private Brick _brick;
    [SerializeField] private Brick spawnPoint;
    [SerializeField] private List<Brick> brickList;
    [SerializeField] private float distance;
    public LayerMask layerBrick;

    private float horizontal, vertical;
    private Vector3 mouseUp, mouseDown;
    private bool isMoving;
    private Vector3 endPoint;


    //private int move;
    private void Start()
    {
        _brick = GameObject.FindObjectOfType<Brick>();
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
                    speed = 20;
                }
                //vuot xuong duoi
                else
                {
                    direct = Direct.Back;
                    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    endPoint= Vector3.back;
                    speed = 20;


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
                    speed = 20;

                }
                //vuot sang trai
                else
                {
                    direct = Direct.Left;
                    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
                    endPoint = Vector3.left;
                    speed = 20;
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

   


    public void CollectBrick()
    {
        
        Brick brick = Instantiate(_brick, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y + 1.5f, spawnPoint.transform.position.z),transform.rotation, this.transform);
        brick.transform.localScale = new Vector3(1.2f, 0.09f, 1.4f);
        //if (brick.color == brickList[0].color)
        //{
        //    brick.RemoveBrickOnGround();
        //}
        brickList.Add(brick);
        for(int i = 0; i < brickList.Count; i++)
        {
            brick.name = $"Brick On Hand ({i})" ;
        }
        UpdateBrick();
    }

    public void UnBrick()
    {
        int index = brickList.Count - 1;

        if(index >= 0)
        {
            Brick brick = brickList[index];
            brickList.Remove(brick);
            //Destroy(brick.gameObject);
            //brick.gameObject.SetActive(false);
            brick.gameObject.tag = "Untagged";
            brick.transform.parent = null;
            brick.gameObject.SetActive(true);
            brick.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 6f, gameObject.transform.position.z );
            
        }
        //brick.transform.SetParent(null);
        //brick.transform.position = new Vector3(unBrick.transform.position.x, unBrick.transform.position.y, unBrick.transform.position.z);

        //UpdateBrick();
    }

    private void UpdateBrick()
    {
        spawnPoint = brickList[brickList.Count - 1];
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Brick")
        {
            if (brickList.Count > 0)
            {
                if(other.GetComponent<Brick>().color == brickList[0].color)
                {
                    other.GetComponent<Brick>().RemoveBrickOnGround() ;
                    CollectBrick();
                    
                }
            }
            else
            {
                other.GetComponent<Brick>().RemoveBrickOnGround();
                CollectBrick();
            }
        }

        if (other.tag == "UnBrick")
        {
            UnBrick();
        }

    }
}
