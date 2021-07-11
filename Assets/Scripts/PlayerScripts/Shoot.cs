using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject gunPos;
    public GameObject bulletPrefab;
    public KeyCode shootKey;

    [SerializeField] private float shootsPerSecond = 1;
    [SerializeField] private float randomShootAngelMinMax = 10f;

    private float _nextShootTimeHolder;
    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (Input.GetKey(shootKey) && Time.time > _nextShootTimeHolder && !GetComponent<GuiltyValue>().inDialog)
        {

            Quaternion shootRotation = gunPos.transform.rotation;

            float randomShootAngle = Random.Range(-randomShootAngelMinMax, randomShootAngelMinMax);

            shootRotation.eulerAngles = new Vector3(0, 0, shootRotation.eulerAngles.z + randomShootAngle);

            Instantiate(bulletPrefab, gunPos.transform.position, shootRotation);

            _nextShootTimeHolder = 1 / shootsPerSecond + Time.time;
        }
    }
}
