using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed;

    void Update()
    {
        transform.Rotate(Time.deltaTime * new Vector3(0, rotationSpeed, 0));
    }
}
