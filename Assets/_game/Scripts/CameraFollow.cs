using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float value;

    private void LateUpdate()
    {
        Vector3 position = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, position, value * Time.deltaTime);
    }
}
