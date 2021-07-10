using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject gunPos;
    public GameObject bulletPrefab;
    public KeyCode shootKey;

    [SerializeField] private float shootsPerSecond = 1;

    private float _nextShootTimeHolder;

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (Input.GetKey(shootKey) && Time.time > _nextShootTimeHolder)
        {
            Instantiate(bulletPrefab, gunPos.transform.position, gunPos.transform.rotation);

            _nextShootTimeHolder = 1 / shootsPerSecond + Time.time;
        }
    }
}
