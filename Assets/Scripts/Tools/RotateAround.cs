using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public float rotationSpeed;
    public Transform target;

    void Update()
    {
        transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
