using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Camera Shake Values")]
    [Range(0, 1)]
    [SerializeField] private float cameraShakeAmt = .1f;
    [Range(0, 1)]
    [SerializeField] private float cameraShakeLenght = .1f;

    [SerializeField] private float _bulletMoveSpeed = 15f;

    [SerializeField] private int _bulletDamage = 25;


    private void Start()
    {
        FindObjectOfType<GameManager>().shootSound.Play();
        GameObject.Find("Camera").GetComponentInChildren<CameraShake>().Shake(cameraShakeAmt, cameraShakeLenght);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * _bulletMoveSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Wall") || collision.CompareTag("Enemy")
            || collision.gameObject.layer == 12 || collision.CompareTag("Boss")) // 12 == Door Layer
        {
            playeSoundEffect();
            PlayExplosionEffect();
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Silence")
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<CheckerHealthSystem>().TakeDamage(_bulletDamage);
            collision.gameObject.GetComponent<Checker>().playerDetected = true;
        }
    }

    private void PlayExplosionEffect()
    {
        GameManager gameManager = GameObject.FindWithTag("GM").GetComponent<GameManager>();

        gameManager.PlayParticle(gameManager.bulletExplosionPrefab, transform.position, transform.rotation);
    }

    private void playeSoundEffect()
    {
        FindObjectOfType<GameManager>().hitSound.Play();
    }
}
