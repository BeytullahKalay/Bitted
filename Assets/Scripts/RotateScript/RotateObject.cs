using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 10f;
    void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }
}
