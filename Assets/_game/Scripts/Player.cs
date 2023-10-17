using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public enum Direct { Forward, Back, Right, Left, None}
public class Player : MonoBehaviour
{
    //[SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private List<Brick> listBrickPrefab;
    [SerializeField] private Brick spawnPoint;
    [SerializeField] private Brick sp;
    [SerializeField] private List<Brick> brickList;
    [SerializeField] private float distance;
    [SerializeField] private Transform startPoint;
    public LayerMask layerBrick;

    private float horizontal, vertical;
    private Vector3 mouseUp, mouseDown;
    //private bool isLeft = false;
    //private bool isRight = false;
    //private bool isFwd = false;
    //private bool isBack = false;
    private Vector3 endPoint;
    private int currentColor;


    //private int move;
    private void Start()
    {
        OnInit();
    }

    private void Update()
    {

        Move();
        CheckEndPoint(endPoint);
        if (brickList.Count <= 0)
        {
            spawnPoint = sp;
        }


    }

    private void OnInit()
    {
        speed = 0;
        ClearBrick();
        gameObject.transform.position = startPoint.position;
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
                    speed = 50;
                }
                //vuot xuong duoi
                else
                {
                    direct = Direct.Back;
                    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    endPoint= Vector3.back;
                    speed = 50;


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
                    speed = 50;

                }
                //vuot sang trai
                else
                {
                    direct = Direct.Left;
                    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
                    endPoint = Vector3.left;
                    speed = 50;
                }
            }
        }
        return direct;
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

    }

    public void CollectBrick()
    {
        currentColor = (int)brickList[0].color;
        Brick brick = Instantiate(listBrickPrefab[currentColor], new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y + 1.5f, spawnPoint.transform.position.z), transform.rotation, this.transform);
        brick.transform.localScale = new Vector3(1.2f, 0.09f, 1.4f);

        brickList.Add(brick);
        //for (int i = 0; i < brickList.Count; i++)
        //{
        //    brick.name = $"Brick On Hand ({i})";
        //}
        UpdateBrick();
    }

    public void UnBrick()
    {
        int index = brickList.Count - 1;

        if (index >= 0)
        {
            Brick brick = brickList[index];
            brickList.Remove(brick);
            //Destroy(brick.gameObject);
            //brick.gameObject.SetActive(false);
            brick.gameObject.tag = "Untagged";
            brick.transform.parent = null;
            brick.gameObject.SetActive(true);
            brick.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 6f, gameObject.transform.position.z);

        }
    }

    private void ClearBrick()
    {
        for(int i = 0; i < brickList.Count; i++)
        {
            Destroy(brickList[i].gameObject);
        }
        brickList.Clear();
    }
    private void UpdateBrick()
    {
        spawnPoint = brickList[brickList.Count - 1];
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(ConstantTag.BRICK))
        {
            if (brickList.Count > 0)
            {
                currentColor = (int)brickList[0].color;
                if (currentColor == (int)other.GetComponent<Brick>().color)
                {
                    
                    other.GetComponent<Brick>().RemoveBrickOnGround() ;
                    CollectBrick();

                }
                else
                {
                    return;
                }
            }
            else
            {
                other.GetComponent<Brick>().RemoveBrickOnGround();
                Brick brick = Instantiate(listBrickPrefab[(int)other.GetComponent<Brick>().color], 
                    new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y + 1.5f, spawnPoint.transform.position.z), transform.rotation, this.transform);
                brick.transform.localScale = new Vector3(1.2f, 0.09f, 1.4f);
                brickList.Add(brick);
            }
        }

        if (other.CompareTag(ConstantTag.UNBRICK))
        {
            UnBrick();
            other.tag = "Untagged";
        }

    }

    public static class ConstantTag
    {
        public static string BRICK = "Brick";
        public static string UNBRICK = "UnBrick";
    }
}
