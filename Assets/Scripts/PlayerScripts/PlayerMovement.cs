using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 6f;
    [SerializeField] private float _turnSpeed = 6f;


    private Vector2 moveInput;


    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Vertical");
        moveInput.y = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * _moveSpeed * moveInput.x * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.forward * _turnSpeed * -moveInput.y);
    }
}
