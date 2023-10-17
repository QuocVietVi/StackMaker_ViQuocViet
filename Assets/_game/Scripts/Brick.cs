using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Colors 
{ 
    GREEN =0 , 
    ORANGE =1
}
public class Brick : MonoBehaviour
{

    public Colors color;
    private void Start()
    {
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    public void RemoveBrickOnGround()
    {
        gameObject.SetActive(false);
    }
}
